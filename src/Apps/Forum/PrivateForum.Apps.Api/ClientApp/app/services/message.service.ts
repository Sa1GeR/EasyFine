import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";

import { MessageModel } from "../models";

@Injectable()
export class MessageService {
  constructor(public http: HttpClient) { }

  public sendMessage(message: MessageModel) : Observable<any> {
    return this.http.post('api/message/create', message);
  }

  public editMessage(message: MessageModel) : Observable<any> {
    return this.http.put('api/message/edit', message);
  }

  public deleteMessage(id: number) {
    return this.http.delete('api/message/delete/' + id);
  }
}