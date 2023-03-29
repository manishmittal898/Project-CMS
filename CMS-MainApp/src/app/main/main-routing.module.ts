import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { HomeComponent } from './home/home.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { PolicyComponent } from './policy/policy.component';
import { PricingComponent } from './pricing/pricing.component';
import { RequestDemoComponent } from './request-demo/request-demo.component';
import { TryForFreeComponent } from './try-for-free/try-for-free.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [{ component: HomeComponent, path: '' },
    { component: HomeComponent, path: 'home' },
    { component: AboutUsComponent, path: 'about-us' },
    { component: ContactUsComponent, path: 'contact-us' },
    { component: PolicyComponent, path: 'policy' },
    { component: PricingComponent, path: 'pricing' },
    { component: RequestDemoComponent, path: 'request-demo' },
    { component: TryForFreeComponent, path: 'Freedemo' },
  ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
