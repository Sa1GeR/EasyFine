import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import 'rxjs/add/operator/switchMap';

import { LoginService } from "../../services";
import { LoginModel } from "../../models";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  constructor(public loginService: LoginService, public route: ActivatedRoute) { }

  loginModel: LoginModel;
  registerModel: RegisterModel;

  actionInProgress = false;

 
  login() {
    this.actionInProgress = true;
    this.loginService.login(this.loginModel).subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open('Logged in.', null, {
          duration: 2000
        });
      }, 
      err => {
        this.actionInProgress = false;
        this.snackBar.open('Error trying to login.', null, {
          duration: 2000
        });
      }
    );
  }

  logout() {
    this.actionInProgress = true;
    this.loginService.logour().subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open('Logged out.', null, {
          duration: 2000
        });
      }, 
      err => {
        this.actionInProgress = false;
        this.snackBar.open('Error trying to log out.', null, {
          duration: 2000
        });
      }
    );
  }
register() {
    this.actionInProgress = true;
    this.loginService.register(this.registerModel).subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open('Registered.', null, {
          duration: 2000
        });
      }, 
      err => {
        this.actionInProgress = false;
        this.snackBar.open('Error trying to register.', null, {
          duration: 2000
        });
      }
    );
  }
}