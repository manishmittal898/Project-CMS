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
import { CheckboxModule } from 'primeng/checkbox';
import { SliderModule } from 'primeng/slider';
import { CalendarModule } from 'primeng/calendar';
import { ProductCardComponent } from '../../../Main/product-list/product-card/product-card.component';



const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent,
  ProductCardComponent
];

const installedModule = [
  NgxPaginationModule,
  NgSelectModule,

  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5
  }),
  CheckboxModule,
  SliderModule,
  CalendarModule,
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
    installedModule],
    providers:[]
})
export class FeatureModule { }
