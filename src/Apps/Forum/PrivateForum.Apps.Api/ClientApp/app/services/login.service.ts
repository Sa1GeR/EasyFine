import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";

import { LoginModel, RegisterModel} from "../models";
import { UserService } from "./user.service";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class LoginService {
  public isLoggedIn: BehaviorSubject<boolean>;

  constructor(public http: HttpClient, public userService: UserService) { 
    this.isLoggedIn = new BehaviorSubject(!!this.getToken());
  }


  public login(loginModel: LoginModel) : Observable<any> {
    return this.http.post<string>('api/accounts/login', loginModel).map(res => { 
      this.writeToken(res);
      this.isLoggedIn.next(true);
      this.userService.retryUser();
    });
  }

  public logout() : Observable<any> {
    localStorage.removeItem('token');
    this.isLoggedIn.next(false);
    return of();
  }

  public register(registerModel: RegisterModel) {
    return this.http.post<string>('api/accounts/register', registerModel).map(res => { 
      this.writeToken(res);
      this.isLoggedIn.next(true);
      this.userService.retryUser();
    });
  }

  public writeToken(token: string): string {
    localStorage.setItem('token', token);
    return token;
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }
}
