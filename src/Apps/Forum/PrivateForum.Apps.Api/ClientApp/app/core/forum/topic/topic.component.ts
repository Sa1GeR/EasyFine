import { Component, Input, OnChanges, SimpleChanges, EventEmitter, Output } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { TopicService } from "../../../services/topic.service";
import { TopicModel, MessageModel } from "../../../models";
import { Observable } from "rxjs/Observable";
import { MessageService } from "../../../services/message.service";
import { UserService } from "../../../services";
import { ProfileComponent } from "../../profile/profile.component";
import { MatDialog } from "@angular/material";


export const EDITOR_CONFIG = {
  "editable": true,
  "spellcheck": true,
  "height": "800",
  "minHeight": "800",
  "width": "auto",
  "minWidth": "0",
  "translate": "yes",
  "enableToolbar": true,
  "showToolbar": true,
  "placeholder": "Write new comment...",
  "imageEndPoint": "",
  "toolbar": [
      ["bold", "italic", "underline", "strikeThrough", "superscript", "subscript"],
      ["fontName", "fontSize", "color"],
      ["justifyLeft", "justifyCenter", "justifyRight", "justifyFull", "indent", "outdent"],
      ["cut", "copy", "delete", "removeFormat", "undo", "redo"],
      ["paragraph", "blockquote", "removeBlockquote", "horizontalLine", "orderedList", "unorderedList"],
      ["link", "unlink"]
  ]
}

@Component({
  selector: 'app-topic',
  templateUrl: './topic.component.html',
  styleUrls: ['./topic.component.scss']
})
export class TopicComponent implements OnChanges {
  @Input() topicId: number;
  @Output() close: EventEmitter<void> = new EventEmitter();
  topic: TopicModel;
  readonly config = EDITOR_CONFIG;


  activeMessage = new MessageModel();

    constructor(public topicService: TopicService, public dialog: MatDialog, public route: ActivatedRoute, public messageService: MessageService, public userService: UserService) {}

  ngOnChanges(changes: SimpleChanges) {
    let change = changes['topicId'];
    if (change) {
      this.loadTopic(this.topicId);
    }
  }

  loadTopic(topicId?) {
    let id = topicId ? topicId : this.topicId;
    this.topicService.getTopic(id).subscribe(topic => this.topic = topic);
  }

  getUrl(): Observable<string> {
    return this.route.url.map(segments => segments.join('/'));
  }

  sendMessage() {
    this.userService.getCurrentUser().switchMap(user => {
      this.activeMessage.authorId = user.id;
      this.activeMessage.topicId = this.topic.id;
      return this.messageService.sendMessage(this.activeMessage);
    }).subscribe(res => {
      this.loadTopic();
      this.activeMessage = new MessageModel();
    }, err => {
      this.loadTopic();
      this.activeMessage = new MessageModel();
    });
    
  }

  replyToMessage(replyId: number) {
    this.activeMessage = new MessageModel();
    this.activeMessage.replyId = replyId;
  }

  removeReplyId() {
    this.activeMessage.replyId = null;
  }

  closeTopic() {
    this.close.emit();
    }


    openUserProfile(userId: number) {
        this.openUserProfileWindow({ userId: userId });
    }
    

    private openUserProfileWindow(data) {
        this.dialog.open(ProfileComponent, {
            width: '450px',
            data: data
        });
    }


}