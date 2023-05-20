import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Offer } from 'src/app/models/offer.model';
import { Location } from 'src/app/models/location.model';
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { LocationService } from 'src/app/services/location.service';
import { Tag } from 'src/app/models/tag.model';
import { TagService } from 'src/app/services/tag.service';
import { TransportationTypeService } from 'src/app/services/transportation-type.service';
import { OfferTypeService } from 'src/app/services/offer-type.service';
import { OfferService } from 'src/app/services/offer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { OfferRequestService } from 'src/app/services/offer-request.service';

@Component({
  selector: 'app-offer-upsert',
  templateUrl: './offer-upsert.component.html',
  styleUrls: ['./offer-upsert.component.scss'],
  providers: [DatePipe]
})
export class OfferUpsertComponent implements OnInit {

  offer: Offer = new Offer();
  offerId: number = 0;

  offerRequestId: number = 0;

  isOfferRequest: boolean = false;

  transportationTypes: any[] = [];
  offerTypes: any[] = [];
  locations: any[] = [];
  tags: any[] = [];

  locationsValid: boolean = false;
  tagsValid: boolean = false;
  availableSpotsValid: boolean = false;
  priceValid: boolean = false;
  startDateValid: boolean = false;
  endDateValid: boolean = false;

  dropdownSettings: IDropdownSettings = {};
  selectedTags: any[] = [];
  selectedLocations: any[] = [];

  constructor(public snackBar: MatSnackBar, private locationService: LocationService,
    private tagService: TagService, private transportationTypeService: TransportationTypeService, 
    private offerTypeService: OfferTypeService, private offerService: OfferService, 
    private offerRequestService: OfferRequestService, 
    private router: Router, private route: ActivatedRoute, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.offerId = Number(this.route.snapshot.paramMap.get('id'));
    this.offerRequestId = Number(this.route.snapshot.paramMap.get('requestId'));
    if (this.offerRequestId != 0)
      this.isOfferRequest = true;
    this.getDataFromBackend(this.isOfferRequest);
    this.setDropdownSettings();
  }

  setDropdownSettings() {
    this.dropdownSettings = {
      idField: 'item_id',
      textField: 'item_text',
      enableCheckAll: false,
      allowSearchFilter: true
    };
  }

  getDataFromBackend(isOfferRequest: boolean) {
    let obj = {
      page: 0,
      pageSize: 10,
      getAll: true
    }

    if (isOfferRequest) 
    {
      this.offerRequestService.getById(this.offerRequestId).subscribe(x => 
        {
          this.offer = x.transferObject;
          this.offer.price = x.transferObject.maxPrice;
          this.offer.availableSpots = x.transferObject.spotNumber;
          this.offer.tags = [];
          this.offer.startDate = this.datePipe.transform(this.offer.startDate, 'yyyy-MM-dd')!;
          this.offer.endDate = this.datePipe.transform(this.offer.endDate, 'yyyy-MM-dd')!;
          
          this.selectedLocations = this.offer.locations.map((location: any) => {
            return {
              item_id: this.locations.find(l => l.item_text == location)?.item_id ?? 0,
              item_text: location
            };
          });

          this.tagService.getAll(obj).subscribe(x => 
            {
              this.tags = x.data.map((item: Tag) => {
                return {
                  item_id: item.id,
                  item_text: item.name
                }
              });
            });

          this.offerTypeService.getAll(obj).subscribe(x => 
            {
              this.offerTypes = x.data;
            });

          this.validateFields();
        });
    } 
    else {
      this.locationService.getAll(obj).subscribe(x => 
        {
          this.locations = x.data.map((item: Location) => {
            return {
              item_id: item.id,
              item_text: item.name
            }
          });
        });
  
      this.tagService.getAll(obj).subscribe(x => 
        {
          this.tags = x.data.map((item: Tag) => {
            return {
              item_id: item.id,
              item_text: item.name
            }
          });
          if (this.offerId) {
            this.getOfferFromBackend();
          }
        });
  
      this.transportationTypeService.getAll(obj).subscribe(x => 
        {
          this.transportationTypes = x.data;
        });
  
      this.offerTypeService.getAll(obj).subscribe(x => 
        {
          this.offerTypes = x.data;
        });
    }
    
  }

  getOfferFromBackend() {
    this.offerService.getById(this.offerId, -1).subscribe(x => {
      if (x?.status == 200) {
        this.offer = x.transferObject;
        this.offer.startDate = this.datePipe.transform(this.offer.startDate, 'yyyy-MM-dd')!;
        this.offer.endDate = this.datePipe.transform(this.offer.endDate, 'yyyy-MM-dd')!;

        this.validateFields();

        this.selectedTags = this.offer.tags.map((tag: any) => {
          return {
            item_id: this.tags.find(t => t.item_text == tag)?.item_id ?? 0,
            item_text: tag
          };
        });

        this.selectedLocations = this.offer.locations.map((location: any) => {
          return {
            item_id: this.locations.find(l => l.item_text == location)?.item_id ?? 0,
            item_text: location
          };
        });
      }
      else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
      
    }, error => {
      this.snackBar.open(error?.error?.title, 'OK', {duration: 2500});
    });
  }

  save() {
    if (!this.locationsValid || !this.tagsValid || !this.availableSpotsValid || !this.priceValid || !this.startDateValid || !this.endDateValid)
      return;

    if (this.offerId == 0)
      this.createOffer();
    else 
      this.editOffer();
  }

  createOffer() {
    if (this.isOfferRequest)
      this.offer.offerRequestId = this.offerRequestId;
    this.offerService.add(this.offer).subscribe(x => 
      {
        if (x.status === 200) {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.router.navigate(['offers']);
        } else
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }, error => {
        this.snackBar.open(error?.message, 'OK', {duration: 2500});
      });
  }

  editOffer() {
    this.offerService.edit(this.offer, this.offerId).subscribe(x => 
      {
        if (x.status === 200) {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.router.navigate(['offers']);
        } else
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }, error => {
        this.snackBar.open(error?.message, 'OK', {duration: 2500});
      }); 
  }

  deleteOfferRequest() {
    this.offerRequestService.delete(this.offerRequestId).subscribe(); 
  }

  onLocationSelect(e: any) {
    this.offer.locations.push(e.item_text);
    this.locationsValid = true;
  }

  onLocationDeselect(e: any) {
    var i = this.offer.locations.indexOf(e.item_text);
    this.offer.locations.splice(i, 1);
    if (this.offer.locations.length == 0)
      this.locationsValid = false;
  }

  onTagSelect(e: any) {
    this.offer.tags.push(e.item_text);
    this.tagsValid = true;
  }

  onTagDeselect(e: any) {
    var i = this.offer.tags.indexOf(e.item_text);
    this.offer.tags.splice(i, 1);
    if (this.offer.tags.length == 0)
      this.tagsValid = false;
  }

  validateFields() {
    this.validateAvailableSpots();
    this.validatePrice();
    this.validateDates();
    this.validateTags();
    this.validateLocations();
  }

  validateAvailableSpots() {
    if (this.offer.availableSpots > 0)
      this.availableSpotsValid = true;
    else 
      this.availableSpotsValid = false;
  }

  validatePrice() {
    if (this.offer.price > 0)
      this.priceValid = true;
    else 
      this.priceValid = false;
  }

  validateDates() {
    if (new Date(this.offer.startDate).getTime() < new Date().getTime())
      this.startDateValid = false;
    else
      this.startDateValid = true;

    if (new Date(this.offer.startDate).getTime() >= new Date(this.offer.endDate).getTime())
      this.endDateValid = false;
    else
      this.endDateValid = true;
  }

  validateTags() {
    if (this.offer.tags.length > 0)
      this.tagsValid = true;
    else 
      this.tagsValid = false;
  }

  validateLocations() {
    if (this.offer.locations.length > 0)
      this.locationsValid = true;
    else 
      this.locationsValid = false;
  }
}
