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
import { CartSidebarComponent } from '../../Page/nav-bar/cart-sidebar/cart-sidebar.component';
import { SaveAddressComponent } from 'src/app/Main/user-account/my-address/save-address/save-address.component';
import { MyAccountComponent } from 'src/app/Main/user-account/my-account/my-account.component';
import { MyOrdersComponent } from 'src/app/Main/user-account/my-orders/my-orders.component';
import { MyWishlistComponent } from 'src/app/Main/user-account/my-wishlist/my-wishlist.component';
import { UserAccountComponent } from 'src/app/Main/user-account/user-account.component';
import { MyAddressComponent } from 'src/app/Main/user-account/my-address/my-address.component';
import { BrowserModule } from '@angular/platform-browser';



const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  CommonModule,
  RouterModule
]

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent,
  CartSidebarComponent,
  ProductCardComponent,
  MyAddressComponent,
  SaveAddressComponent,

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
})
export class FeatureModule { }
