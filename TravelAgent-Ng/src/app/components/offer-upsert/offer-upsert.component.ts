import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Offer } from 'src/app/models/offer.model';
import { Location } from 'src/app/models/location.model';
import { IDropdownSettings, } from 'ng-multiselect-dropdown';
import { LocationService } from 'src/app/services/location.service';
import { Tag } from 'src/app/models/tag.model';
import { TagService } from 'src/app/services/tag.service';
import { TransportationTypeService } from 'src/app/services/transportation-type.service';
import { OfferTypeService } from 'src/app/services/offer-type.service';
import { OfferService } from 'src/app/services/offer.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offer-upsert',
  templateUrl: './offer-upsert.component.html',
  styleUrls: ['./offer-upsert.component.scss']
})
export class OfferUpsertComponent implements OnInit {

  offer: Offer = new Offer();

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

  constructor(private toastr: ToastrService, private locationService: LocationService,
    private tagService: TagService, private transportationTypeService: TransportationTypeService, 
    private offerTypeService: OfferTypeService, private offerService: OfferService, private router: Router) { }

  ngOnInit(): void {

    this.getDataFromBackend();
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

  getDataFromBackend() {

    let obj = {
      searchFilter: "",
      page: 1,
      pageSize: 10
    }

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

  save() {
    if (!this.locationsValid || !this.tagsValid || !this.availableSpotsValid || !this.priceValid || !this.startDateValid || !this.endDateValid)
      return;

    this.offerService.add(this.offer).subscribe(x => 
      {
        if (x.status === 200){
          this.toastr.success(x.message);
          this.router.navigate(['offers']);
        } else
          this.toastr.error(x.message);
      }, error => {
        this.toastr.error('An error occurred. ' + error.message);
      });
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

    if (new Date(this.offer.startDate).getTime() >= new Date(this.offer.endDate).getTime() )
      this.endDateValid = false;
    else
      this.endDateValid = true;
  }
}
