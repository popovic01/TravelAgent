import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OfferService } from 'src/app/services/offer.service';
import { OfferComponent } from '../offer/offer.component';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-offer-review',
  templateUrl: './offer-review.component.html',
  styleUrls: ['./offer-review.component.scss'],
  providers: [OfferComponent]
})
export class OfferReviewComponent implements OnInit {

  public offerId: number = 0;
  public offer: any;

  constructor(private offerService: OfferService, public authService: AuthService,
    private route: ActivatedRoute, public offerComponent: OfferComponent,
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.route.params.subscribe(x => {
      this.offerId = x['id'];
    });

    let userId = -1;
    if (this.authService.getCurrentUser()?.UserId && !this.authService.isAdmin())
      userId = Number(this.authService.getCurrentUser().UserId);
    this.offerService.getById(this.offerId, userId).subscribe(x => {
      if (x?.status == 200) {
        this.offer = x.transferObject;
      } else {
        this.snackBar.open(x?.message, 'OK', {duration: 2500});
      }
    }, error => {
      this.snackBar.open(error?.statusText, 'OK', {duration: 2500});
    });
  }

}
