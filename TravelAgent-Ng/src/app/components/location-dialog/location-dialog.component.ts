import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { LocationService } from 'src/app/services/location.service';
import { Location } from 'src/app/models/location.model';

@Component({
  selector: 'app-location-dialog',
  templateUrl: './location-dialog.component.html',
  styleUrls: ['./location-dialog.component.scss']
})
export class LocationDialogComponent implements OnInit {

//oznaka za operaciju o kojoj se radi - 1: insert, 2: update, 3: delete
public flag!: number;
subscription!: Subscription;

constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<LocationDialogComponent>,  
  @Inject(MAT_DIALOG_DATA) public data: Location, public locationService: LocationService) { }

ngOnInit(): void {}

public add(): void {
  this.subscription = this.locationService.add(this.data).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open(x.message, 'OK', {duration: 2500});
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom dodavanja nove lokacije', 'Zatvori', {duration: 2500})
  });
}

public update(): void {
  this.subscription = this.locationService.edit(this.data, this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open(x.message, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom modifikacije lokacije', 'Zatvori', {duration: 2500})
  });
}

public delete(): void {
  this.subscription = this.locationService.delete(this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open(x.message, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom brisanja postojeće lokacije', 'Zatvori', {duration: 2500})
  });
}

public cancel(): void {
  this.dialogRef.close();
}

ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

}
