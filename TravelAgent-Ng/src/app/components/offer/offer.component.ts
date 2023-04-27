import { Component, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { formatDate } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offer',
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.scss']
})
export class OfferComponent implements OnInit {

  public offers: any = [];

  public currentPage: number = 1;
  public pageSize: number = 10;
  public count: number = 0;

  public search: string = '';
  public dateFrom: any = new Date();
  public dateTo: any = new Date();

  constructor(private offerService: OfferService,
    private router: Router) { }

  ngOnInit(): void {
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
      clientId: 0
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

  public pageChange(value: any) {
    this.currentPage = value;
    this.loadData();
  }

}
