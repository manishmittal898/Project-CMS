import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer.component';
import { CustomerAddEditComponent } from './customer-add-edit/customer-add-edit.component';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerAddressComponent } from './customer-address/customer-address.component';

const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: CustomerComponent },
  { path: 'add', component: CustomerAddEditComponent },
  { path: 'edit/:id', component: CustomerAddEditComponent },
  { path: 'detail/:id', component: CustomerDetailComponent },
  { path: 'address/:id', component: CustomerAddressComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
