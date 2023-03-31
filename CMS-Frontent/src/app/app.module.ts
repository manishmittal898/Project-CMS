import { AppInterceptor } from './Shared/app.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeatureModule } from './Shared/Module/feature/feature.module';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { LocationStrategy, PathLocationStrategy, CommonModule } from '@angular/common';
import { BaseAPIService } from './Shared/Services/base-api.service';
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { LoginComponent } from './Shared/Page/login/login.component';
import { RegisterComponent } from './Shared/Page/register/register.component';
import { MainModule } from './Main/main.module';
import { ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    AppComponent,
    HtmlComponent,
    LoginComponent,
    RegisterComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FeatureModule,
    MainModule
  ],
  providers: [BaseAPIService,  { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
    { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent],

})
export class AppModule { }
