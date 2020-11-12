import { Component, ViewChild, OnInit } from '@angular/core';
import { HistoryService } from '../services/history/history.service';
import { BookHistory } from '../models/BookHistory';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table'


@Component({
  selector: 'app-book-history',
  templateUrl: './book-history.component.html'
})


export class BookHistoryComponent implements OnInit {
  removeGroup: boolean;
  groupByField: string;
  dataSource = new MatTableDataSource<BookHistory | Group>([]);
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  _alldata: any[];
  displayedColumns: string[];
  columns: any[];
  groupByColumns: string[] = [];

  constructor(private historyService: HistoryService) {

    this.columns = [{
      field: 'id',
      value: 'Id'
    }, {
      field: 'description',
      value: 'Description'
    }, {
      field: 'dateChanged',
      value: 'Date Changed'
    }, {
      field: 'entityBookId',
      value: 'Entity Id'
    }];
    this.displayedColumns = this.columns.map(column => column.field);
    this.groupByColumns = [];

  }


  ngOnInit() {
    this.historyService.getAll()
      .subscribe(
        data => {
          if (data.hasError) {
            alert(data.message);
          } else {
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
            this._alldata = data.item;
            this.dataSource.data = this.addGroups(this._alldata, this.groupByColumns);
            this.dataSource.filter = "";
          }
        },
        error => {
          console.log(error);
        });
  }

  filterValue = "";
  applyFilter(value: string) {
    this.filterValue = value;
    this.dataSource.filter = this.filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  groupBy(event, column) {
    event.stopPropagation();
    this.unGroupBy(event);
    this.checkGroupByColumn(column, true);
    this.dataSource.data = this.addGroups(this._alldata, this.groupByColumns);
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
    this.groupByField = this.columns.filter(x => x.field == column)[0].value;
    this.removeGroup = true;
  }

  checkGroupByColumn(field, add) {
    let found = null;
    for (const column of this.groupByColumns) {
      if (column === field) {
        found = this.groupByColumns.indexOf(column, 0);
      }
    }
    if (found != null && found >= 0) {
      if (!add) {
        this.groupByColumns.splice(found, 1);
      }
    } else {
      if (add) {
        this.groupByColumns.push(field);
      }
    }
  }

  unGroupBy(event) {
    event.stopPropagation();
    this.removeGroup = false;
    this.groupByColumns = [];
    this.dataSource.data = this.addGroups(this._alldata, this.groupByColumns);
    this.dataSource.filter = this.filterValue.trim().toLowerCase();
  }


  getDataRowVisible(data: any): boolean {
    const groupRows = this.dataSource.data.filter(
      row => {
        if (!(row instanceof Group)) {
          return false;
        }
        let match = true;
        this.groupByColumns.forEach(column => {
          if (!row[column] || !data[column] || row[column] !== data[column]) {
            match = false;
          }
        });
        return match;
      }
    );

    if (groupRows.length === 0) {
      return true;
    }
    const parent = groupRows[0] as Group;
    return parent.visible && parent.expanded;
  }

  

  addGroups(data: any[], groupByColumns: string[]): any[] {
    const rootGroup = new Group();
    rootGroup.expanded = true;
    return this.getSublevel(data, 0, groupByColumns, rootGroup);
  }

  getSublevel(data: any[], level: number, groupByColumns: string[], parent: Group): any[] {
    if (level >= groupByColumns.length) {
      return data;
    }
    const groups = this.uniqueBy(
      data.map(
        row => {
          const result = new Group();
          result.level = level + 1;
          result.parent = parent;
          for (let i = 0; i <= level; i++) {
            result[groupByColumns[i]] = row[groupByColumns[i]];
          }
          return result;
        }
      ),
      JSON.stringify);

    const currentColumn = groupByColumns[level];
    let subGroups = [];
    groups.forEach(group => {
      const rowsInGroup = data.filter(row => group[currentColumn] === row[currentColumn]);
      group.totalCounts = rowsInGroup.length;
      const subGroup = this.getSublevel(rowsInGroup, level + 1, groupByColumns, group);
      subGroup.unshift(group);
      subGroups = subGroups.concat(subGroup);
    });
    return subGroups;
  }

  uniqueBy(a, key) {
    const seen = {};
    return a.filter((item) => {
      const k = key(item);
      return seen.hasOwnProperty(k) ? false : (seen[k] = true);
    });
  }

  isGroup(index, item): boolean {
    return item.level;
  }
}


export class Group {
  level = 0;
  parent: Group;
  expanded = true;
  totalCounts = 0;
  get visible(): boolean {
    return !this.parent || (this.parent.visible && this.parent.expanded);
  }
}

