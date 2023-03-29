import { UIDesignComponent } from './uidesign.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from '../Shared/Page/login/login.component';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
 
const routes: Routes = [
  {
    path: '',
    component: UIDesignComponent,
    children: [{
      component: LoginComponent, path: '',

    },

    {
      component: CartComponent, path: 'cart',

    },

    {
      component: CheckoutComponent, path: 'checkout',

    }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UIDesignRoutingModule { }
