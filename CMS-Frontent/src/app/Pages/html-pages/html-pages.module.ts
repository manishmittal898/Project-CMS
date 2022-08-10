
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HtmlPagesRoutingModule } from './html-pages-routing.module';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { MainPageComponent } from './main-page.component';
import { FeatureModule } from '../../Shared/Module/feature/feature.module';
import { CategoryProductListComponent } from './product-list/product-detail/category-product-list/category-product-list.component';
import { ProductFilterComponent } from './product-list/product-filter/product-filter.component';


@NgModule({
  declarations: [HomeComponent,
    ProductListComponent,
    ProductDetailComponent,
    MainPageComponent,
    CategoryProductListComponent,
    ProductFilterComponent],
  imports: [
    CommonModule,
    HtmlPagesRoutingModule,
    FeatureModule
  ],
  exports: []
})
export class HtmlPagesModule { }
