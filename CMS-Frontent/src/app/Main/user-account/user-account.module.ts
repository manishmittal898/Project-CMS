import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserAccountRoutingModule } from './user-account-routing.module';
import { MyAccountComponent } from './my-account/my-account.component';
import { FeatureModule } from '../../Shared/Module/feature/feature.module';
import { UserAccountComponent } from './user-account.component';


@NgModule({
  declarations: [MyAccountComponent, UserAccountComponent],
  imports: [
    CommonModule,
    UserAccountRoutingModule,
    FeatureModule
  ]
})
export class UserAccountModule { }
