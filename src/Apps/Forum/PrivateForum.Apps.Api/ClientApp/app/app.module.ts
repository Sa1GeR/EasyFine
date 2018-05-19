import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutes } from './app.routing';
import { AppComponent } from './app.component';

import { SharedModule } from './shared';
import { LayoutModule } from './layout';
import { CoreModule } from './core/core.module';
import { AppGuard } from './app.resolver';
import { UserService } from './services';

@NgModule({
  declarations: [
    AppComponent
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
    AppGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
