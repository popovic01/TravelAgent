import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Offer } from 'src/app/models/offer.model';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-offer-dialog',
  templateUrl: './offer-dialog.component.html',
  styleUrls: ['./offer-dialog.component.scss']
})
export class OfferDialogComponent implements OnInit, OnDestroy {

subscription!: Subscription;

constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<OfferDialogComponent>,  
  @Inject(MAT_DIALOG_DATA) public data: Offer, public offerService: OfferService) { }

ngOnInit(): void {}

public delete(): void {
  this.subscription = this.offerService.delete(this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open(x.message, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom brisanja ponude', 'Zatvori', {duration: 2500})
  });
}

public cancel(): void {
  this.dialogRef.close();
}

ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

}
