import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShopComponent } from './shop.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { AuthGuard } from 'src/app/Shared/Services/Core/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: ShopComponent,
    children: [
      { component: CartComponent, path: 'cart', },
      { component: CheckoutComponent, path: 'checkout', canActivate: [AuthGuard] }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
