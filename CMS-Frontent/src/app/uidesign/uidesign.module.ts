import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UIDesignRoutingModule } from './uidesign-routing.module';
import { LoginComponent } from './login/login.component';
import { UIDesignComponent } from './uidesign.component';
import { FeatureModule } from '../Shared/Module/feature/feature.module';
import { RegisterComponent } from './register/register.component';

@NgModule({
  declarations: [UIDesignComponent, LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    UIDesignRoutingModule,
    FeatureModule
  ]
})
export class UIDesignModule { }
