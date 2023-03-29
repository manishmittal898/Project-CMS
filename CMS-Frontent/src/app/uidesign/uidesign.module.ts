import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UIDesignRoutingModule } from './uidesign-routing.module';

import { UIDesignComponent } from './uidesign.component';
import { FeatureModule } from '../Shared/Module/feature/feature.module';

import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';

@NgModule({
  declarations: [UIDesignComponent,  CartComponent, CheckoutComponent],
  imports: [
    CommonModule,
    UIDesignRoutingModule,
    FeatureModule
  ]
})
export class UIDesignModule { }
