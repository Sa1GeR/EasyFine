import { Component, OnInit } from "@angular/core";
import { MatDatepickerModule } from '@angular/material/datepicker';
import "rxjs/add/operator/switchMap";

import { LoginService } from "../../services";
import { LoginModel, RegisterModel } from "../../models";
import { MatSnackBar } from "@angular/material";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent {
  constructor(
      public loginService: LoginService,
      public route: Router,
    public snackBar: MatSnackBar
  ) {}

  loginModel: LoginModel = new LoginModel();
  registerModel: RegisterModel = new RegisterModel();

  actionInProgress = false;

  login() {
    this.actionInProgress = true;
    this.loginService.login(this.loginModel).subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open("Logged in.", null, {
          duration: 2000
          });
          this.route.navigateByUrl("/forum");
      },
      err => {
        this.actionInProgress = false;
        this.snackBar.open("Inalid login/password pair", null, {
          duration: 3000
        });
      }
    );
  }

  logout() {
    this.actionInProgress = true;
    this.loginService.logout().subscribe(
      () => {
        this.actionInProgress = false;
        this.snackBar.open("Logged out.", null, {
          duration: 2000
        });
      },
      err => {
        this.actionInProgress = false;
        this.snackBar.open("Error trying to log out.", null, {
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
        this.snackBar.open("Registered.", null, {
          duration: 2000
          });
          this.route.navigateByUrl("/forum");

      },
      err => {
        this.actionInProgress = false;
        this.snackBar.open("Error trying to register.", null, {
          duration: 2000
        });
      }
    );
  }
}
