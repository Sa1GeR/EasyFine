import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/switchMap';

import { UserService } from "../../services";
import { ProfileModel } from "../../models";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit{
  constructor(public userService: UserService, public route: ActivatedRoute, public snackBar: MatSnackBar) { }

  profile: ProfileModel;

  actionInProgress = false;

  ngOnInit() {
    this.route.paramMap.switchMap(paramMap => {
      let id = paramMap.get('id');
      return this.userService.getProfile(id);
    }).subscribe(profile => this.profile = profile);
  }

  blockUser() {
    this.actionInProgress = true;
    this.userService.blockUser(this.profile.id).subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open('User blocked', null, {
          duration: 2000
        });
      }, 
      err => {
        this.actionInProgress = false;
        this.snackBar.open('User couldn\'t be blocked', null, {
          duration: 2000
        });
      }
    );
  }

  deleteUser() {
    this.actionInProgress = true;
    this.userService.deleteUser(this.profile.id).subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open('User deleted', null, {
          duration: 2000
        });
      }, 
      err => {
        this.actionInProgress = false;
        this.snackBar.open('User couldn\'t be deleted', null, {
          duration: 2000
        });
      }
    );
  }
}