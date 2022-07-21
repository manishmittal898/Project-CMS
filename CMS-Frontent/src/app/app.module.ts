import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeatureModule } from './Shared/Module/feature/feature.module';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseAPIService } from './Shared/Services/base-api.service';
@NgModule({
  declarations: [
    AppComponent,
    HtmlComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FeatureModule
  ],
  providers: [BaseAPIService,


    { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent],

})
export class AppModule { }
