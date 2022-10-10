import { UIDesignComponent } from './uidesign.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CartComponent } from './cart/cart.component';

const routes: Routes = [
  {
    path: '',
    component: UIDesignComponent,
    children: [{
      component: LoginComponent, path: '',

    },
    {
      component: LoginComponent, path: 'login',

    },
    {
      component: RegisterComponent, path: 'register',

    },
    {
      component: CartComponent, path: 'cart',

    }

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UIDesignRoutingModule { }
