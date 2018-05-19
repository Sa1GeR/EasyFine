import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Resolve } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { ForumModel } from '../../models';
import { ForumService } from '../../services';

@Injectable()
export class ForumResolver implements Resolve<ForumModel> {
  constructor(public forumService: ForumService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ForumModel> {
    let forumId = route.paramMap.get('id');

    return forumId ? this.forumService.getForum(forumId) : this.forumService.getRoot(); 
  }
}