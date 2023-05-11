import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-offer-review',
  templateUrl: './offer-review.component.html',
  styleUrls: ['./offer-review.component.scss']
})
export class OfferReviewComponent implements OnInit {

  public offerId: number = 0;
  public offer: any;

  constructor(private offerService: OfferService,
    private route: ActivatedRoute,
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.route.params.subscribe(x => {
      this.offerId = x['id'];
    });

    this.offerService.getById(this.offerId).subscribe(x => {
      if (x?.status == 200) {
        this.offer = x.transferObject;
        console.log(this.offer);
      } else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
    }, error => {
      this.snackBar.open(error?.statusText, 'OK', {duration: 2500});
    });
  }

}
