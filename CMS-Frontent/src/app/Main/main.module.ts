import { MainRoutingModule } from './main-routing.module';
import { UserAccountModule } from './user-account/user-account.module';
import { MainComponent } from './main.component';
import { HomeComponent } from './home/home.component';
import { ProductFilterComponent } from './product-list/product-filter/product-filter.component';
import { CMSPageContentComponent } from './cmspage-content/cmspage-content.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FeatureModule } from '../Shared/Module/feature/feature.module';
import { ProductListComponent } from './product-list/product-list.component';
import { CategoryProductListComponent } from './product-list/product-detail/category-product-list/category-product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component'; 
import { ProductSectionComponent } from './home/product-section/product-section.component';

@NgModule({
  declarations: [
    HomeComponent,
    ProductListComponent,
    ProductDetailComponent,
    MainComponent,
    CategoryProductListComponent,
    ProductFilterComponent,
    CMSPageContentComponent,

    ProductSectionComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    UserAccountModule,
    FeatureModule
  ],
  exports: []
})
export class MainModule { }
