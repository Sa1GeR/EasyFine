import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";

import { LoginModel, RegisterModel} from "../models";

@Injectable()
export class LoginService {
  constructor(public http: HttpClient) { }

  public login(loginModel: LoginModel) : Observable<any> {
    return this.http.post<string>('api/accounts/login', loginModel).map(res => this.writeToken(res));
  }

  public logout() : Observable<any> {
    localStorage.removeItem('token');
    return this.http.post('api/accounts/logout', {});
  }

  public register(registerModel: RegisterModel) {
    return this.http.post('api/accounts/register', registerModel);
  }

  public writeToken(token: string): string {
    localStorage.setItem('token', token);
    return token;
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }
}
