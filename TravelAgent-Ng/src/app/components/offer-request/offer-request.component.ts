import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { OfferRequestService } from 'src/app/services/offer-request.service';

@Component({
  selector: 'app-offer-request',
  templateUrl: './offer-request.component.html',
  styleUrls: ['./offer-request.component.scss']
})
export class OfferRequestComponent implements OnInit {

  public offerRequests: any = [];

  public currentPage: number = 1;
  public pageSize: number = 10;
  public count: number = 0;

  public dateFrom: any = new Date();
  public dateTo: any = new Date();

  constructor(private offerRequestService: OfferRequestService, public snackBar: MatSnackBar,
    private router: Router, public authService: AuthService, 
    public dialog: MatDialog, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get('id');
    });
    this.dateFrom = formatDate(new Date().setDate(new Date().getDate() - 7), "yyyy-MM-dd", "en");
    this.dateTo = formatDate(new Date(), "yyyy-MM-dd", "en");
    this.loadData();
  }

  loadData(page: any = null) {
    if (page)
      this.currentPage = page;

    let obj = this.getTableParams();

    this.offerRequestService.getAll(obj).subscribe(x => 
      {
        this.offerRequests = x.data;
        this.count = x.count;
      });
  }

  delete(id: number) {
    this.offerRequestService.delete(id).subscribe(x => 
      {
        if (x.status === 200){
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
        this.loadData();
      } else
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }, error => {
        this.snackBar.open(error?.message, 'OK', {duration: 2500});
      }); 
  }

  private getTableParams() {  
    let obj = {
      pageSize: this.pageSize,
      page: this.currentPage,
      clientId: this.authService.isAdmin() ? null : Number(this.authService.getCurrentUser()?.UserId)
    }    
            
    return obj;
  }

  isLoggedIn(): boolean {
    if (this.authService.getCurrentUser())
      return true;
    this.snackBar.open('Morate se prijaviti', 'OK', {duration: 2500});
    return false;
  }

}
