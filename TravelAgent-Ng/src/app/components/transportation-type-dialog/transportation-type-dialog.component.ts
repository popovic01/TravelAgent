import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { TransportationType } from 'src/app/models/transportationType';
import { TransportationTypeService } from 'src/app/services/transportation-type.service';
@Component({
  selector: 'app-transportation-type-dialog',
  templateUrl: './transportation-type-dialog.component.html',
  styleUrls: ['./transportation-type-dialog.component.scss']
})
export class TransportationTypeDialogComponent implements OnInit {

//oznaka za operaciju o kojoj se radi - 1: insert, 2: update, 3: delete
public flag!: number;
subscription!: Subscription;

constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<TransportationTypeDialogComponent>,  
  @Inject(MAT_DIALOG_DATA) public data: TransportationType, public transportationTypeService: TransportationTypeService) { }

ngOnInit(): void {}

public add(): void {
  this.subscription = this.transportationTypeService.add(this.data).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno dodat tipa prevoza: ' + this.data.name, 'OK', {duration: 2500});
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom dodavanja novog tipa prevoza!', 'Zatvori', {duration: 2500})
  });
}

public update(): void {
  this.subscription = this.transportationTypeService.edit(this.data, this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno izmenjen tip prevoza: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom modifikacije tipa prevoza!', 'Zatvori', {duration: 2500})
  });
}

public delete(): void {
  this.subscription = this.transportationTypeService.delete(this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno obrisan tipa prevoza: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom brisanja postojećeg tipa prevoza!', 'Zatvori', {duration: 2500})
  });
}

public cancel(): void {
  this.dialogRef.close();
}

ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

}
