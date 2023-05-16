import { Component, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { formatDate } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { OfferDialogComponent } from '../offer-dialog/offer-dialog.component';

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

  constructor(private offerService: OfferService, public snackBar: MatSnackBar,
    private router: Router, public authService: AuthService, 
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

  isLoggedIn(): boolean {
    if (this.authService.getCurrentUser())
      return true;
    this.snackBar.open('Morate se prijaviti', 'OK', {duration: 2500});
    return false;
  }

}
