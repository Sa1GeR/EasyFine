import { Component, OnInit } from "@angular/core";
import { UserService, LoginService } from "../../services";
import { UserModel } from "../../models";
import { Router } from "@angular/router";
import { ProfileComponent } from "../../core/profile/profile.component";
import { MatDialog } from "@angular/material";

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
        public router: Router,
        public dialog: MatDialog
    ) { }

    ngOnInit(): void {
        this.loginService.isLoggedIn.subscribe(isLoggedIn => {
            if (isLoggedIn) {
                this.userService.getCurrentUser().subscribe(user => {
                    this.isLoggedIn = isLoggedIn;
                    this.user = user;
                    this.userService
                        .getProfile(user.id.toString())
                        .subscribe(profile => (this.profile = profile));
                });
            } else {
                this.user = null;
                this.profile = null;
                this.isLoggedIn = isLoggedIn;
            }
        });
    }

    openProfile() {
        this.dialog.open(ProfileComponent, {
            width: '450px',
            data: { userId: this.user.id }
        });
    }


    logout() {
        this.loginService.logout();
        this.router.navigateByUrl("/auth");
    }
}
