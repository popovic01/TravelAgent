import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.route.params.subscribe(x => {
      this.offerId = x['id'];
    });

    this.offerService.getById(this.offerId).subscribe(x => {
      if (x?.status == 200) {
        this.offer = x.transferObject;
        console.log(this.offer);
      } else {
        this.toastr.error(x?.message);
      }
    }, error => {
      this.toastr.error(error?.statusText);
    });
  }

}
