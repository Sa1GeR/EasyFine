import { Component, ViewChild, AfterViewInit } from "@angular/core";
import { ProfileModel } from "../../models";
import { MatTableDataSource, MatPaginator } from "@angular/material";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements AfterViewInit {
  dataSource = new MatTableDataSource<ProfileModel>(profiles);
  displayedColumns = ['id', 'name', 'email', 'isBlocked'];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngAfterViewInit() {
    

    this.dataSource.paginator = this.paginator;
    
  }
}

const profiles: ProfileModel[] = [
  { id: 1, firstName: 'Test', middleName: '', lastName: 'Test', email: 'test@mail.com', isBlocked: false },
  { id: 1, firstName: 'Test', middleName: '', lastName: 'Test', email: 'test@mail.com', isBlocked: false },
  { id: 1, firstName: 'Test', middleName: '', lastName: 'Test', email: 'test@mail.com', isBlocked: false },
  { id: 1, firstName: 'Test', middleName: '', lastName: 'Test', email: 'test@mail.com', isBlocked: false },
  { id: 1, firstName: 'Test', middleName: '', lastName: 'Test', email: 'test@mail.com', isBlocked: false }
];