import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { SharedModule } from "../shared";

import { CoreRoutes } from "./core.routing";

import { NotFoundComponent } from "./not-found/not-found.component";
import { ForumComponent } from "./forum/forum.component";
import { TopicComponent } from "./forum/topic/topic.component";
import { ProfileComponent } from "./profile/profile.component";
import { UpsertForumDialogComponent } from "./dialogs/upsert-forum/upsert-forum.dialog.component";

import { ForumResolver } from "./forum/forum.resolver";
import { ForumService, LoginService } from "../services";
import { TopicService } from "../services/topic.service";
import { UpsertTopicDialogComponent } from "./dialogs/upsert-topic/upsert-topic.dialog.component";
import { MessageService } from "../services/message.service";
import { SafePipe } from "./forum/topic/safe.pipe";
import { LoginComponent } from "./login/login.component";


@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    CoreRoutes
  ],
  declarations: [
    ProfileComponent,
    NotFoundComponent,
    ForumComponent,
    TopicComponent,
    LoginComponent,
    UpsertForumDialogComponent,
    UpsertTopicDialogComponent,
    SafePipe
  ],
  entryComponents: [
    UpsertForumDialogComponent,
    UpsertTopicDialogComponent
  ],
  providers: [
    ForumService,
    ForumResolver,
    TopicService,
    MessageService,
    LoginService
  ]
})
export class CoreModule { }