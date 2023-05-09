import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Tag } from 'src/app/models/tag.model';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-tag-dialog',
  templateUrl: './tag-dialog.component.html',
  styleUrls: ['./tag-dialog.component.scss']
})
export class TagDialogComponent implements OnInit {

//oznaka za operaciju o kojoj se radi - 1: insert, 2: update, 3: delete
public flag!: number;
subscription!: Subscription;

constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<TagDialogComponent>,  
  @Inject(MAT_DIALOG_DATA) public data: Tag, public tagService: TagService) { }

ngOnInit(): void {}

public add(): void {
  this.subscription = this.tagService.add(this.data).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno dodat tag: ' + this.data.name, 'OK', {duration: 2500});
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom dodavanja novog taga!', 'Zatvori', {duration: 2500})
  });
}

public update(): void {
  this.subscription = this.tagService.edit(this.data, this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno izmenjen tag: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom modifikacije taga!', 'Zatvori', {duration: 2500})
  });
}

public delete(): void {
  this.subscription = this.tagService.delete(this.data.id).subscribe(x => {
    if (x.status === 200) {
      this.snackBar.open('Uspešno obrisan tag: ' + this.data.name, 'OK', {duration: 2500})
    } else
      this.snackBar.open(x.message);
  }, () => {
      this.snackBar.open('Došlo je do greške prilikom brisanja postojećeg taga!', 'Zatvori', {duration: 2500})
  });
}

public cancel(): void {
  this.dialogRef.close();
}

ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

}
