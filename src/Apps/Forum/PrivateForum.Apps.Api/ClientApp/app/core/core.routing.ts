import { RouterModule, Routes } from '@angular/router';

import { ProfileComponent } from './profile/profile.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ForumComponent } from './forum/forum.component';
import { ForumResolver } from './forum/forum.resolver';

const routes: Routes = [
  { path: 'profile/:id', component: ProfileComponent },
  { path: 'forum/:id', component: ForumComponent, resolve: { forumData: ForumResolver}, runGuardsAndResolvers: 'always' },
  { path: 'forum', component: ForumComponent, resolve: { forumData: ForumResolver }, runGuardsAndResolvers: 'always' },
  { path: 'not-found', component: NotFoundComponent },
  { path: '**', redirectTo: '/forum' }
];

export const CoreRoutes = RouterModule.forChild(routes);