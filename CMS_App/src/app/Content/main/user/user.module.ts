import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { UserAddEditComponent } from './user-add-edit/user-add-edit.component';


@NgModule({
  declarations: [
    UserComponent,
    UserDetailComponent,
    UserAddEditComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
