import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { of } from "rxjs/observable/of";
import "rxjs/add/operator/share";

import { TopicModel } from "../models";

@Injectable()
export class TopicService {
  constructor(public http: HttpClient) { }

  public getTopic(id: number): Observable<TopicModel> {
    return this.http.get<TopicModel>('api/topic/get/' + id);
  }

  public createTopic(topic: TopicModel) : Observable<any> {
    let t = {
      ...topic,
      message: topic.messages[0]
    };

    return this.http.post('api/topic/create', t);
  }

  public editTopic(topic: TopicModel) : Observable<any> {
    return this.http.put('api/topic/edit', topic);
  }

  public deleteTopic(id: number) {
    return this.http.delete('api/topic/delete/' + id);
  }
}