import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile/profile.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ForumComponent } from './forum/forum.component';
import { ForumResolver } from './forum/forum.resolver';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { UserListComponent } from './user-list/user-list.component';

const routes: Routes = [
  { path: 'profile/:id', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'forum/:id', component: ForumComponent, resolve: { forumData: ForumResolver}, runGuardsAndResolvers: 'always', canActivate: [AuthGuard] },
  { path: 'forum', component: ForumComponent, resolve: { forumData: ForumResolver }, runGuardsAndResolvers: 'always', canActivate: [AuthGuard] },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'auth', component: LoginComponent },
  { path: 'user-list', component: UserListComponent },
  { path: '**', redirectTo: '/forum' }
];

export const CoreRoutes = RouterModule.forChild(routes);