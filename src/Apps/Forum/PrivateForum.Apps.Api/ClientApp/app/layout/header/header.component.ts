import { Component, OnInit } from "@angular/core";
import { UserService, LoginService } from "../../services";
import { UserModel } from "../../models";
import { Router } from "@angular/router";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;
  user: UserModel;
  profile: any;

  constructor(
    public userService: UserService,
    public loginService: LoginService,
    public router: Router
  ) { }

  ngOnInit(): void {
    this.loginService.isLoggedIn.subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      if (this.isLoggedIn) {
        this.userService.getCurrentUser().subscribe(user => {
          this.user = user;
          this.userService
            .getProfile(user.id.toString())
            .subscribe(profile => (this.profile = profile));
        });
      }
    });
  }

  openProfile() {}

  logout() {
    localStorage.removeItem("token");
    this.router.navigateByUrl("/auth");
  }
}
