import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductsRoutingModule } from './products-routing.module';
import { ProductsComponent } from './products.component';
import { TryForFreeComponent } from './try-for-free/try-for-free.component';
import { FeatureModule } from '../Shared/Features/feature.module';


@NgModule({
  declarations: [
    ProductsComponent,
    TryForFreeComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    FeatureModule,
  ]
})
export class ProductsModule { }
