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
    return this.http.post('api/accounts/login', loginModel);
  }

  public logout() : Observable<any> {
    return this.http.post('api/message/edit', message);
  }

  public register(registerModel: RegisterModel) {
    return this.http.post('api/accounts/register', registerModel);
  }
}
