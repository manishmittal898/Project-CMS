import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeatureModule } from './Shared/Module/feature/feature.module';
import { HtmlComponent } from './Shared/Page/html/html.component';
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
  providers: [],
  bootstrap: [AppComponent],

})
export class AppModule { }
