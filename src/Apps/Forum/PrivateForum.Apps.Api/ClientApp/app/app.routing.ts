import { RouterModule, Routes, PreloadAllModules } from '@angular/router';
import { AppGuard } from './app.resolver';

const routes: Routes = [
  { path: '', loadChildren: 'app/core/core.module#CoreModule' },
  { path: '**', redirectTo: '' }
];

export const AppRoutes = RouterModule.forRoot(routes, { enableTracing: true});

