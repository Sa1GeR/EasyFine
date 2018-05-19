import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ForumModel } from "../models";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";
import { Subject } from "rxjs/Subject";

@Injectable()
export class ForumService {
  constructor(public http: HttpClient) { }

  public activeForum: Subject<ForumModel> = new Subject();
  private lastId: string;

  public getForum(id: string): Observable<ForumModel> {
    return this.http.get<ForumModel>('api/forum/get/' + id).map(forum => {
      this.lastId = id;
      this.activeForum.next(forum);
      return forum;
    });
  }

  public triggerReload() {
    this.getForum(this.lastId).subscribe();
  }

  public getRoot(): Observable<ForumModel> {
    return this.http.get<ForumModel>('api/forum/get/root').map(forum => {
      this.lastId = forum.id.toString();
      this.activeForum.next(forum);
      return forum;
    });
  }

  public createForum(forum: ForumModel) : Observable<any> {
    return this.http.post('api/forum/create', forum);
  }

  public editForum(forum: ForumModel) : Observable<any> {
    return this.http.put('api/forum/edit', forum);
  }

  public deleteForum(id: number) {
    return this.http.delete('api/forum/delete/' + id);
  }
}