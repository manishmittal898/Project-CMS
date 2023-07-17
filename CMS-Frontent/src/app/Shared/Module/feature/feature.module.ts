import { NavBarComponent } from './../../Page/nav-bar/nav-bar.component';
import { FooterComponent } from './../../Page/footer/footer.component';
import { HeaderComponent } from './../../Page/header/header.component';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { NgSelectModule } from "@ng-select/ng-select";
import { NgxPaginationModule } from "ngx-pagination";
import { ToastrModule } from "ngx-toastr";
import { CheckboxModule, SliderModule } from "primeng-lts";

const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent

];

const installedModule = [
  NgxPaginationModule,
  NgSelectModule,
  CheckboxModule,
  SliderModule,
  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5
  }),
]

@NgModule({
  declarations: [component],
  imports: [
    CommonModule,
    installedModule,
    commonModules,
  ],
  exports: [
    component,
    commonModules,
    installedModule]
})
export class FeatureModule { }
