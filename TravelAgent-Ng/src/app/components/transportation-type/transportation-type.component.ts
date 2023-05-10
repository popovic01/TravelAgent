import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { TransportationType } from 'src/app/models/transportationType';
import { TransportationTypeService } from 'src/app/services/transportation-type.service';
import { TransportationTypeDialogComponent } from '../transportation-type-dialog/transportation-type-dialog.component';

@Component({
  selector: 'app-transportation-type',
  templateUrl: './transportation-type.component.html',
  styleUrls: ['./transportation-type.component.scss']
})
export class TransportationTypeComponent implements OnInit, OnDestroy {

    //kolone koje se prikazuju u tabeli
    displayedColumns = ['id', 'name', 'actions'];
    dataSource!: MatTableDataSource<TransportationType>;
    subscription!: Subscription;
    @ViewChild(MatSort, {static: false}) sort!: MatSort;
    @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;

    public currentPage: number = 1;
    public pageSize: number = 10;
    public count: number = 0;
    public search: string = '';

  constructor(private transportationTypeService: TransportationTypeService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    let obj = this.getTableParams();
    this.subscription = this.transportationTypeService.getAll(obj).subscribe(x => {
      this.dataSource = new MatTableDataSource(x.data);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    },
    (error: Error) => {
      console.log(error.name + ' ' + error.message);
    });
  }

  private getTableParams() {  
    let obj = {
      pageSize: this.pageSize,
      page: this.currentPage,
      searchFilter: this.search,
    }
            
    return obj;
  }

  public openDialog(flag: number, id?: number, name?: string) {
    const dialogRef = this.dialog.open(TransportationTypeDialogComponent, {data: {id, name}});
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe(result => {
      if (result == 1)
        this.loadData();
    })
  }

  applyFilter(filterValue: any) {
    filterValue = filterValue.target.value;
    filterValue = filterValue.trim();
    filterValue = filterValue.toLocaleLowerCase();
    this.dataSource.filter = filterValue;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}