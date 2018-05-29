import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";

import { UserModel, ProfileModel, Roles } from "../models";

@Injectable()
export class UserService {
  constructor(public http: HttpClient) {
    this.request = this.http
      .get<UserModel>("api/user/me")
      .share();

    this.request.subscribe(user => {
      this.currentUser = user;
    });
  }

  public retryUser() {
    this.request = this.http
      .get<UserModel>("api/user/me")
      .share();

    this.request.subscribe(user => {
      this.currentUser = user;
    });
  }

  private currentUser: UserModel;
  private request: Observable<UserModel>;

  public getCurrentUser(): Observable<UserModel> {
    return this.currentUser ? of(this.currentUser) : this.request;
  }

  public getProfile(id: string): Observable<ProfileModel> {
    return this.http.get<ProfileModel>('api/user/profile/' + id);
  }

  public blockUser(id: number) {
    return this.http.post('api/user/block/' + id, {});
  }

  public deleteUser(id: number) {
    return this.http.delete('api/user/delete/' + id);
  }

  public isAdmin(user: UserModel = null)  : Observable<boolean> { return this.isUserInRole(user, "Administrator"); }
  public isClient(user: UserModel = null) : Observable<boolean> { return this.isUserInRole(user, "Client"); }

  private isUserInRole(user: UserModel, role: "Administrator" | "Client"): Observable<boolean> {
      return user ? of(user.role == role) : this.getCurrentUser().map(user => user ? user.role == role : false);
  }

  public getUsers(): Observable<ProfileModel[]> {
    return this.http.get<ProfileModel[]>('api/user/getall');
  }
}
