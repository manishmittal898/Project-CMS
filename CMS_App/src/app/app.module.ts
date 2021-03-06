import { CommonModule, LocationStrategy, PathLocationStrategy } from "@angular/common";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { AppInterceptor } from "./Shared/Helper/app.interceptor";
import { BaseAPIService } from "./Shared/Helper/base-api.service";
import { LoaderService } from "./Shared/Helper/loader.service";
import { SharedModule } from "./Shared/Helper/shared/shared.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './Content/Common/login/login.component';
import { MainModule } from "./Content/main/main.module";
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    MainModule,
  ],
  providers: [BaseAPIService,

    LoaderService, { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true },
    { provide: LocationStrategy, useClass: PathLocationStrategy }],
  bootstrap: [AppComponent]
})
export class AppModule { }
