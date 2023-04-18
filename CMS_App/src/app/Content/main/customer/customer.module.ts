import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { CustomerDetailComponent } from './customer-detail/customer-detail.component';
import { CustomerAddEditComponent } from './customer-add-edit/customer-add-edit.component';
import { CustomerAddressComponent } from './customer-address/customer-address.component';
import { OrderDetailsComponent } from './customer-detail/order-details/order-details.component';



@NgModule({
  declarations: [
    CustomerComponent,
    CustomerDetailComponent,
    CustomerAddEditComponent,
    CustomerAddressComponent,
    OrderDetailsComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    SharedModule
  ]
})
export class CustomerModule { }
