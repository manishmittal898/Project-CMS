import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { PricingComponent } from './pricing/pricing.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';
import { CommonsModule } from '../common/commons.module';



@NgModule({
  declarations: [
    HomeComponent,
    AboutComponent,
    PricingComponent,
    ContactUsComponent,
    PrivacyPolicyComponent
  ],
  imports: [
    CommonModule,
    CommonsModule
  ]
})
export class PagesModule { }
