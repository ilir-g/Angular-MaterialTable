import { Component, ViewChild, OnInit } from '@angular/core';
import { BookEntitiesService } from 'src/app/services/entities/book-entities.service';
import { BookEntities } from 'src/app/models/BookEntities';

import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table'

@Component({
  selector: 'app-book-entities',
  templateUrl: './book-entities.component.html'
})
 
export class BookEntitiesComponent implements OnInit {

  public displayedColumns = ['id', 'title', 'description', 'publishDate','authors','action'];
  dataSource = new MatTableDataSource<BookEntities>();
  @ViewChild(MatSort, { static:true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private bookEntitiesService: BookEntitiesService) {
   
  }
    ngOnInit(): void {
      this.bookEntitiesService.getAll()
        .subscribe(
          data => {
            if (data.hasError) {
              alert(data.message);
            }
            else {
          
              this.dataSource = new MatTableDataSource(data.item);
              this.dataSource.paginator = this.paginator;
              this.dataSource.sort = this.sort;
            }
          },
          error => {
            alert(error);
          });
    }

  applyFilter(event: Event) {

    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
