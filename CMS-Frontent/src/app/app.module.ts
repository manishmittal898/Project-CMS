import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeatureModule } from './Shared/Module/feature/feature.module';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { LocationStrategy, PathLocationStrategy, CommonModule } from '@angular/common';
import { BaseAPIService } from './Shared/Services/base-api.service';
@NgModule({
  declarations: [
    AppComponent,
    HtmlComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FeatureModule
  ],
  providers: [BaseAPIService,  { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent],

})
export class AppModule { }
