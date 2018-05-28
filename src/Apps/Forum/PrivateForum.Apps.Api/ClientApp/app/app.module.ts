import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutes } from './app.routing';
import { AppComponent } from './app.component';

import { SharedModule } from './shared';
import { LayoutModule } from './layout';
import { CoreModule } from './core/core.module';
import { AppGuard } from './app.resolver';
import { UserService, LoginService } from './services';
import { TokenInterceptor } from './services/token.interceptor';
import { ProfileComponent } from './core/profile/profile.component';

@NgModule({
  declarations: [
      AppComponent,
      ProfileComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutes,
    HttpClientModule,
    SharedModule,
    LayoutModule,
    CoreModule,
    AppRoutes
  ],
  providers: [
    UserService,
    AppGuard,
    LoginService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
