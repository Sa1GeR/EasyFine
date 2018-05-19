import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanLoad, Route, CanActivate, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { UserModel } from './models';
import { UserService } from './services';

@Injectable()
export class AppGuard implements CanLoad, CanActivate {
  constructor(public userService: UserService) { }

  canLoad(route: Route): Observable<boolean> {
    console.log("called");
    return this.userService.getCurrentUser()
      .map(() => true)
      .catch(() => of(false))

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    console.log("called");
    return this.userService.getCurrentUser()
      .map(() => true)
      .catch(() => of(false))
  }
}