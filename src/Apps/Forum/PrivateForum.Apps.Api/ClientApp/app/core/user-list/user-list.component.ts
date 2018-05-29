import { Component, ViewChild, AfterViewInit } from "@angular/core";
import { ProfileModel } from "../../models";
import { MatTableDataSource, MatPaginator } from "@angular/material";
import { UserService } from "../../services";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements AfterViewInit {
  dataSource: MatTableDataSource<ProfileModel>;
  displayedColumns = ['id', 'name', 'email', 'address', 'isBlocked'];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(public userService: UserService) { }

  ngAfterViewInit() {
    this.userService.getUsers().subscribe(res => {
      this.dataSource = new MatTableDataSource<ProfileModel>(res);
      this.dataSource.paginator = this.paginator;
    })
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); 
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
}