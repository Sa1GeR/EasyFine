import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ForumModel } from '../../../models/index';
import { ForumService } from '../../../services/index';

@Component({
    selector: 'app-upsert-forum',
    templateUrl: './upsert-forum.dialog.component.html',
    styleUrls: ['./upsert-forum.dialog.component.scss']
})
export class UpsertForumDialogComponent implements OnInit {
    private successNotification: string = "Changes were applied successfully";
    private failedNotification: string = "Changes were not saved";

    public forum: ForumModel;

    constructor(
        public forumService: ForumService,
        public dialogRef: MatDialogRef<UpsertForumDialogComponent>, 
        @Inject(MAT_DIALOG_DATA) public data: any,
        public snackBar: MatSnackBar
    ) {
        this.forum = data.forum ? data.forum : <ForumModel>{ parentId: data.parentId };;
    }

    ngOnInit(): void { }

    cancel() { this.dialogRef.close(); }

    save(forum) {
        if(!this.validate()) {
            this.snackBar.open("All required fields should be set!", null, { duration: 1000 });
            return;
        }
            
        (!this.forum.id 
            ?   this.forumService.createForum(forum)
            :   this.forumService.editForum(forum)
        )
            .subscribe(
                () => { this.setSuccessNotification(this.dialogRef); this.forumService.triggerReload(); this.dialogRef.close(); },
                () => { this.setFailedNotification(this.dialogRef);  this.forumService.triggerReload(); this.dialogRef.close(); }
            );
    }

    private setSuccessNotification(dialog: MatDialogRef<UpsertForumDialogComponent>) { 
        dialog.afterClosed().subscribe(() =>  { this.snackBar.open(this.successNotification, null, { duration: 2000 }); this.data.cb(); });
    }

    private setFailedNotification(dialog: MatDialogRef<UpsertForumDialogComponent>) { 
        dialog.afterClosed().subscribe(() => this.snackBar.open(this.successNotification, null, { duration: 2000 }));
    }

    private validate(): boolean {
        return !!this.forum.name;
    }
}
