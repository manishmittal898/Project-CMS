import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserAccountComponent } from './user-account.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { MyOdersComponent } from './my-oders/my-oders.component';
import { MyAddressComponent } from './my-address/my-address.component';

const routes: Routes = [
  {
    path: '',
    component: UserAccountComponent,
    children: [
      { component: MyAccountComponent, path: '' },
      { component: MyAccountComponent, path: 'profile' },
      { component: MyOdersComponent, path: 'orders' },
      { component: MyAddressComponent, path: 'address' }


    ]

  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserAccountRoutingModule { }
