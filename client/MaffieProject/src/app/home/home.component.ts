import { Component, OnInit, ViewChild } from '@angular/core';
import { AboneService } from '../Services/abone.service';

import { Abone } from '../Interfaces/abone.model';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  aboneData: Abone[]=[];
  displayedColumns : string[]= ['openingDate','number','address','name','surname','kdv','currencyUnit','id','aboneId','tüketimHesabi','ucretHesabi']
dataSource: MatTableDataSource<Abone> = new MatTableDataSource<Abone>();

kurData: any;

@ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private readonly aboneService:AboneService) {

  }

  ngOnInit(): void {
    this.aboneService.getAbones().subscribe(
      (success)=>{
          this.aboneData=success;
          this.dataSource = new MatTableDataSource<Abone>(this.aboneData);

          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
      }
    )


  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }


}
