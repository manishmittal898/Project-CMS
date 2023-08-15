import { FeatureModule } from 'src/app/Shared/Module/feature/feature.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppInterceptor } from './Shared/Services/Core/app.interceptor';
import { CommonModule, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { LoginComponent } from './Shared/Page/login/login.component';
import { RegisterComponent } from './Shared/Page/register/register.component';
import { BaseAPIService } from './Shared/Services/Core/base-api.service';
import { MainModule } from './Main/main.module';
import { CookieService } from 'ngx-cookie-service';

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
  providers: [BaseAPIService, CookieService, { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
    { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent],

})
export class AppModule { }
