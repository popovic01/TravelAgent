import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.scss']
})
export class ReservationComponent implements OnInit {

  public reservations: any = [];

  public currentPage: number = 1;
  public pageSize: number = 10;
  public count: number = 0;

  public search: string = '';
  public dateFrom: any = new Date();
  public dateTo: any = new Date();

  constructor(private reservationService: ReservationService, public snackBar: MatSnackBar,
    public authService: AuthService) { }

  ngOnInit(): void {
    this.dateFrom = formatDate(new Date().setDate(new Date().getDate() - 7), "yyyy-MM-dd", "en");
    this.dateTo = formatDate(new Date(), "yyyy-MM-dd", "en");
    this.loadData();
  }

  loadData(page: any = null) {
    if (page)
      this.currentPage = page;

    let obj = this.getTableParams();

    let userId = this.authService.isAdmin() == true ? 0 : Number(this.authService.getCurrentUser().UserId);

    this.reservationService.getAll(obj, userId).subscribe(x => 
      {
        console.log(x)
        this.reservations = x.data;
        this.count = x.count;
      });
  }

  private getTableParams() {  
    let obj = {
      pageSize: this.pageSize,
      page: this.currentPage,
      getAll: false
    }
            
    return obj;
  }

}
