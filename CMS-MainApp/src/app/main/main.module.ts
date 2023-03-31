import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { MainComponent } from './main.component';
import { FeatureModule } from '../Shared/Features/feature.module';
import { HomeComponent } from './home/home.component';
import { PricingComponent } from './pricing/pricing.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { PolicyComponent } from './policy/policy.component';
import { RequestDemoComponent } from './request-demo/request-demo.component';
 

@NgModule({
  declarations: [
    MainComponent,
    HomeComponent,
    PricingComponent,
    AboutUsComponent,
    ContactUsComponent,
    PolicyComponent,
    RequestDemoComponent,

  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    FeatureModule,

  ]
})
export class MainModule { }
