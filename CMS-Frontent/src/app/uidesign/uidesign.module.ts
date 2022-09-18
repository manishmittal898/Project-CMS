import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UIDesignRoutingModule } from './uidesign-routing.module';
import { LoginComponent } from './login/login.component';
import { UIDesignComponent } from './uidesign.component';
import { FeatureModule } from '../Shared/Module/feature/feature.module';

@NgModule({
  declarations: [UIDesignComponent, LoginComponent],
  imports: [
    CommonModule,
    UIDesignRoutingModule,
    FeatureModule
  ]
})
export class UIDesignModule { }
