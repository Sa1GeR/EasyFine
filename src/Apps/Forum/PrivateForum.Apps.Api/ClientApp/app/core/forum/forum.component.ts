import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ForumModel, TopicModel } from "../../models";
import { MatDialog } from "@angular/material";
import { UpsertForumDialogComponent } from "../dialogs/upsert-forum/upsert-forum.dialog.component";
import { UserService, ForumService } from "../../services/index";
import { UpsertTopicDialogComponent } from "../dialogs/upsert-topic/upsert-topic.dialog.component";
import { merge } from 'rxjs/observable/merge';
@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.scss']
})
export class ForumComponent implements OnInit {
  topicId: number = null;
  
  forum: ForumModel;

  constructor(public route: ActivatedRoute, public router: Router, public dialog: MatDialog, public userService: UserService, public forumService: ForumService) { }
  
  ngOnInit() {
    merge(
      this.route.data.map(data => data['forumData']),
      this.forumService.activeForum
    )
    .subscribe(forum => {
      this.forum = forum; 
      if (this.forum.deWay)
        this.forum.deWay.reverse();
    });
  }

  openTopic(id: number) {
    this.topicId = id;
  }

  openForum(id: number = null) {
    if(id)
      this.router.navigate(['/forum', id]);
    else 
      this.router.navigate(['/forum']);
  }

  reload() {
    this.forumService.triggerReload();
  }

  createNewForum() {
    this.openForumUpsertWindow({ parentId: this.forum.id, cb: this.reload() });
  }

  editForum() {
    this.openForumUpsertWindow({ forum: this.forum, cb: this.reload() });
  }

  createNewTopic() {
    this.openTopicUpsertWindow({ forumId: this.forum.id, cb: this.reload() });
  }

  editTopic(topic: TopicModel) {
    this.openTopicUpsertWindow({ topic: topic, cb: this.reload() });
  }

  private openForumUpsertWindow(data) {
    this.dialog.open(UpsertForumDialogComponent, {
      width: '450px',
      data: data
    });
  }

  private openTopicUpsertWindow(data) {
    this.dialog.open(UpsertTopicDialogComponent, {
      width: '450px',
      data: data
    });
  }

  closeTopic() {
    this.topicId = null;
  }
}