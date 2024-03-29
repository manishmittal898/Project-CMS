import { MyAccountComponent } from './my-account/my-account.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { UserAccountRoutingModule } from './user-account-routing.module';
import { UserAccountComponent } from './user-account.component';

import { MyOrdersComponent } from './my-orders/my-orders.component';
import { MyWishlistComponent } from './my-wishlist/my-wishlist.component';
import { FeatureModule } from 'src/app/Shared/Module/feature/feature.module';
import { SaveAddressComponent } from './my-address/save-address/save-address.component';
import { MyAddressComponent } from './my-address/my-address.component';

@NgModule({
  declarations: [
    MyAccountComponent,
    UserAccountComponent,
    MyOrdersComponent,
    MyWishlistComponent,
  //  MyAddressComponent
  ],
  imports: [
    CommonModule,
    UserAccountRoutingModule,
    FeatureModule
  ],
})
export class UserAccountModule { }
