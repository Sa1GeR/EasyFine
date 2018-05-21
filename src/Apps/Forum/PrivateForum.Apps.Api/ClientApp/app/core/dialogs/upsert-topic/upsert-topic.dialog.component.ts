import { Component, OnInit, Inject } from '@angular/core';
import { TopicModel, MessageModel } from '../../../models/index';
import { TopicService } from '../../../services/topic.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ForumService, UserService } from '../../../services';
import 'rxjs/add/operator/switchMap';

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
    selector: 'app-upsert-topic',
    templateUrl: './upsert-topic.dialog.component.html',
    styleUrls: ['./upsert-topic.dialog.component.scss']
})
export class UpsertTopicDialogComponent implements OnInit {
    private successNotification: string = "Changes were applied successfully";
    private failedNotification: string = "Changes were not saved";

    readonly config = EDITOR_CONFIG;

    public topic: TopicModel;

    constructor(
        public topicService: TopicService,
        public dialogRef: MatDialogRef<UpsertTopicDialogComponent>, 
        @Inject(MAT_DIALOG_DATA) public data: any,
        public snackBar: MatSnackBar,
        public forumService: ForumService,
        public userService: UserService
    ) {
        this.topic = data.topic ? data.topic : <TopicModel>{ folderId : data.forumId, messages: [<MessageModel>{}] };
    }

    ngOnInit(): void { }

    get headMessage(): MessageModel { 
        if(!(this.topic.messages && this.topic.messages.length))
            return null;

        return this.topic.messages[0];
    }

    cancel() { this.dialogRef.close(); }

    save(topic) {
        if(!this.validate()) {
            this.snackBar.open("All required fields should be set!", null, { duration: 1000 });
            return;
        }

        this.userService.getCurrentUser().switchMap(user => {
            if (!this.topic.messages[0].authorId) {
                this.topic.messages[0].authorId = user.id;
            }
           
            return (!this.topic.id 
                ?   this.topicService.createTopic(topic)
                :   this.topicService.editTopic(topic)
            )
        }) 
        .subscribe(
            () => { this.setSuccessNotification(this.dialogRef); this.forumService.triggerReload(); this.dialogRef.close(); },
            () => { this.setFailedNotification(this.dialogRef); this.forumService.triggerReload(); this.dialogRef.close(); }
        );
    }

    private setSuccessNotification(dialog: MatDialogRef<UpsertTopicDialogComponent>) { 
        dialog.afterClosed().subscribe(() => { this.snackBar.open(this.successNotification, null, { duration: 2000 }); });
    }

    private setFailedNotification(dialog: MatDialogRef<UpsertTopicDialogComponent>) { 
        dialog.afterClosed().subscribe(() => this.snackBar.open(this.successNotification, null, { duration: 2000 }));
    }

    private validate(): boolean {
        return !!(this.topic.header && this.topic.messages && this.topic.messages.length);
    }
}
