import { Component, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { formatDate } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { OfferDialogComponent } from '../offer-dialog/offer-dialog.component';
import { ReservationService } from 'src/app/services/reservation.service';
import { environment } from 'src/environments/environment';
import { Stripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.scss']
})
export class OfferComponent implements OnInit {

  public offers: any = [];

  public isWishlist: boolean = false;

  public currentPage: number = 1;
  public pageSize: number = 10;
  public count: number = 0;

  public search: string = '';
  public dateFrom: any = new Date();
  public dateTo: any = new Date();

  private stripePromise: Promise<Stripe> | undefined;

  constructor(private offerService: OfferService, public snackBar: MatSnackBar,
    private router: Router, public authService: AuthService, private reservationService: ReservationService,
    public dialog: MatDialog, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isWishlist = true;
      }
    });
    this.dateFrom = formatDate(new Date().setDate(new Date().getDate() - 7), "yyyy-MM-dd", "en");
    this.dateTo = formatDate(new Date(), "yyyy-MM-dd", "en");
    this.loadData();
    this.stripePromise = this.loadStripe();
  }

  loadData(page: any = null) {
    if (page)
      this.currentPage = page;

    let obj = this.getTableParams();

    this.offerService.getAll(obj).subscribe(x => 
      {
        console.log(x)
        this.offers = x.data;
        this.count = x.count;
      });
  }

  private getTableParams() {  
    let pageInfo = {
      pageSize: this.pageSize,
      page: this.currentPage,
    }    
    
    let filterParams = {
      searchFilter: this.search,
      // startDate: this.dateFrom,
      // endDate: this.dateTo,
      startDate: "",
      endDate: "",
      locationIds: [],
      tagIds: [],
      userId: this.authService.isAdmin() ? null : Number(this.authService.getCurrentUser()?.UserId),
      isWishlist: this.isWishlist
    };

    let obj = {
      pageInfo: pageInfo,
      filterParams: filterParams
    }
            
    return obj;
  }

  reviewOffer(id: number) {
    this.router.navigate(['/offer-review', id]);
  }

  public openDialog(id?: number, name?: string) {
    const dialogRef = this.dialog.open(OfferDialogComponent, {data: {id, name}});
    dialogRef.afterClosed().subscribe(result => {
      if (result == 1)
        this.loadData();
    })
  }

  public addToWishlist(offerId: number) {
    if (!this.isLoggedIn() || this.authService.isAdmin()) {
      return;
    }
    this.offerService.addToWishlist(offerId, Number(this.authService.getCurrentUser().UserId)).subscribe(x => {
      if (x?.status == 200) {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.loadData();
      } else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
    }, error => {
      this.snackBar.open(error?.statusText, 'OK', {duration: 2500});
    });
  }

  public removeFromWishlist(offerId: number) {
    if (!this.isLoggedIn() || this.authService.isAdmin()) {
      return;
    }
    this.offerService.removeFromWishlist(offerId, Number(this.authService.getCurrentUser().UserId)).subscribe(x => {
      if (x?.status == 200) {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.loadData();
      } else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
    }, error => {
      this.snackBar.open(error?.statusText, 'OK', {duration: 2500});
    });
  }

  async book(id: number) {
    this.isLoggedIn();
    let reservation = {
      offerId: id,
      clientId: Number(this.authService.getCurrentUser().UserId),
      date: formatDate(new Date(), "yyyy-MM-dd", "en"),
      reservationCode: ""
    };
    this.reservationService.add(reservation).subscribe(async x => {
      if (x?.status == 200) {
        const stripe = await this.getStripe();
        stripe.redirectToCheckout({ sessionId: x.transferObject });
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      } else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
    }, error => {
      this.snackBar.open(error?.statusText, 'OK', {duration: 2500});
    });
  }

  private loadStripe(): Promise<Stripe> {
    return new Promise((resolve, reject) => {
      const script = document.createElement('script');
      script.src = 'https://js.stripe.com/v3/';
      script.onload = () => {
        resolve((window as any).Stripe(environment.publishableKey));
      };
      script.onerror = (error) => reject(error);
      document.body.appendChild(script);
    });
  }

  async getStripe(): Promise<Stripe> {
    return this.stripePromise!;
  }

  isLoggedIn(): boolean {
    if (this.authService.getCurrentUser())
      return true;
    this.snackBar.open('Morate se prijaviti', 'OK', {duration: 2500});
    return false;
  }

}
