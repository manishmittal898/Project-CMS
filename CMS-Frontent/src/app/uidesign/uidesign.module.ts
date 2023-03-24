import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UIDesignRoutingModule } from './uidesign-routing.module';
import { LoginComponent } from './login/login.component';
import { UIDesignComponent } from './uidesign.component';
import { FeatureModule } from '../Shared/Module/feature/feature.module';
import { RegisterComponent } from './register/register.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { MyAccountComponent } from './my-account/my-account.component';

@NgModule({
  declarations: [UIDesignComponent, LoginComponent, RegisterComponent, CartComponent, CheckoutComponent, MyAccountComponent],
  imports: [
    CommonModule,
    UIDesignRoutingModule,
    FeatureModule
  ]
})
export class UIDesignModule { }
