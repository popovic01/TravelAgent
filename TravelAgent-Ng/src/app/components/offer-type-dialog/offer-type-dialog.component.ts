import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { OfferType } from 'src/app/models/offerType';
import { OfferTypeService } from 'src/app/services/offer-type.service';

@Component({
  selector: 'app-offer-type-dialog',
  templateUrl: './offer-type-dialog.component.html',
  styleUrls: ['./offer-type-dialog.component.scss']
})
export class OfferTypeDialogComponent implements OnInit {

//oznaka za operaciju o kojoj se radi - 1: insert, 2: update, 3: delete
public flag!: number;
subscription!: Subscription;

constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<OfferTypeDialogComponent>,  
  @Inject(MAT_DIALOG_DATA) public data: OfferType, public offerTypeService: OfferTypeService) { }

ngOnInit(): void {}

public add(): void {
  this.subscription = this.offerTypeService.add(this.data).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno dodat tip ponude: ' + this.data.name, 'OK', {duration: 2500});
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom dodavanja novog tipa ponude!', 'Zatvori', {duration: 2500})
  });
}

public update(): void {
  this.subscription = this.offerTypeService.edit(this.data, this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno izmenjen tip ponude: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom modifikacije tipa ponude!', 'Zatvori', {duration: 2500})
  });
}

public delete(): void {
  this.subscription = this.offerTypeService.delete(this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno obrisan tip ponude: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom brisanja postojećeg tipa ponude!', 'Zatvori', {duration: 2500})
  });
}

public cancel(): void {
  this.dialogRef.close();
}

ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

}
