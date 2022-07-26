
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HtmlPagesRoutingModule } from './html-pages-routing.module';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { MainPageComponent } from './main-page.component';
import { FeatureModule } from '../../Shared/Module/feature/feature.module';


@NgModule({
  declarations: [HomeComponent, ProductListComponent, ProductDetailComponent, MainPageComponent],
  imports: [
    CommonModule,
    HtmlPagesRoutingModule,
    FeatureModule
  ],
  exports: []
})
export class HtmlPagesModule { }
