import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { OfferRequest } from 'src/app/models/offer-request';
import { LocationService } from 'src/app/services/location.service';
import { OfferRequestService } from 'src/app/services/offer-request.service';
import { TransportationTypeService } from 'src/app/services/transportation-type.service';
import { Location } from 'src/app/models/location.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-offer-request-upsert',
  templateUrl: './offer-request-upsert.component.html',
  styleUrls: ['./offer-request-upsert.component.scss'],
  providers: [DatePipe]
})
export class OfferRequestUpsertComponent implements OnInit {

  offerRequest: OfferRequest = new OfferRequest();
  offerRequestId: number = 0;

  transportationTypes: any[] = [];
  locations: any[] = [];

  locationsValid: boolean = false;
  spotNumberValid: boolean = false;
  priceValid: boolean = false;
  startDateValid: boolean = false;
  endDateValid: boolean = false;

  dropdownSettings: IDropdownSettings = {};
  selectedLocations: any[] = [];

  constructor(public snackBar: MatSnackBar, private locationService: LocationService,
    private transportationTypeService: TransportationTypeService, 
    private offerRequestService: OfferRequestService, private router: Router, 
    private route: ActivatedRoute, private datePipe: DatePipe, private authService: AuthService) { }

  ngOnInit(): void {
    this.getDataFromBackend();
    this.setDropdownSettings();
    this.offerRequestId = Number(this.route.snapshot.paramMap.get('id'));
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
      page: 0,
      pageSize: 10,
      getAll: true
    }

    this.locationService.getAll(obj).subscribe(x => 
      {
        this.locations = x.data.map((item: Location) => {
          return {
            item_id: item.id,
            item_text: item.name
          }
        });
        if (this.offerRequestId) {
          this.getOfferRequestFromBackend();
        }
      });

    this.transportationTypeService.getAll(obj).subscribe(x => 
      {
        this.transportationTypes = x.data;
      });
  }

  getOfferRequestFromBackend() {
    this.offerRequestService.getById(this.offerRequestId).subscribe(x => {
      if (x?.status == 200) {
        this.offerRequest = x.transferObject;
        this.offerRequest.startDate = this.datePipe.transform(this.offerRequest.startDate, 'yyyy-MM-dd')!;
        this.offerRequest.endDate = this.datePipe.transform(this.offerRequest.endDate, 'yyyy-MM-dd')!;

        this.validateFields();

        this.selectedLocations = this.offerRequest.locationIds.map((location: any) => {
          return {
            item_id: location,
            item_text: this.locations.find(l => l.item_id == location)?.item_text ?? 0
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
    if (!this.locationsValid || !this.spotNumberValid || !this.priceValid || !this.startDateValid || !this.endDateValid)
      return;

    if (this.offerRequestId == 0)
      this.createOfferRequest();
    else 
      this.editOfferRequest();
  }

  createOfferRequest() {
    this.offerRequest.clientId = Number(this.authService.getCurrentUser().UserId);
    this.offerRequestService.add(this.offerRequest).subscribe(x => 
      {
        if (x.status === 200) {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.router.navigate(['offer-requests']);
        } else
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }, error => {
        this.snackBar.open(error?.message, 'OK', {duration: 2500});
      });
  }

  editOfferRequest() {
    this.offerRequestService.edit(this.offerRequest, this.offerRequestId).subscribe(x => 
      {
        if (x.status === 200){
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.router.navigate(['offers']);
        } else
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }, error => {
        this.snackBar.open(error?.message, 'OK', {duration: 2500});
      }); 
  }

  onLocationSelect(e: any) {
    this.offerRequest.locationIds.push(e.item_id);
    this.locationsValid = true;
  }

  onLocationDeselect(e: any) {
    var i = this.offerRequest.locationIds.indexOf(e.item_id);
    this.offerRequest.locationIds.splice(i, 1);
    if (this.offerRequest.locationIds.length == 0)
      this.locationsValid = false;
  }

  validateFields() {
    this.validateSpotNumber();
    this.validatePrice();
    this.validateDates();
    this.validateLocations();
  }

  validateSpotNumber() {
    if (this.offerRequest.spotNumber > 0)
      this.spotNumberValid = true;
    else 
      this.spotNumberValid = false;
  }

  validatePrice() {
    if (this.offerRequest.maxPrice > 0)
      this.priceValid = true;
    else 
      this.priceValid = false;
  }

  validateDates() {
    if (new Date(this.offerRequest.startDate).getTime() < new Date().getTime())
      this.startDateValid = false;
    else
      this.startDateValid = true;

    if (new Date(this.offerRequest.startDate).getTime() >= new Date(this.offerRequest.endDate).getTime() )
      this.endDateValid = false;
    else
      this.endDateValid = true;
  }

  validateLocations() {
    if (this.offerRequest.locationIds.length > 0)
      this.locationsValid = true;
    else 
      this.locationsValid = false;
  }

}
