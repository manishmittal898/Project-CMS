import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { CartComponent } from './cart/cart.component';
import { FeatureModule } from '../../Shared/Module/feature/feature.module';


@NgModule({
  declarations: [ShopComponent,
    CheckoutComponent,
    CartComponent],
  imports: [
    CommonModule,
    FeatureModule,
    ShopRoutingModule
  ]
})
export class ShopModule { }
