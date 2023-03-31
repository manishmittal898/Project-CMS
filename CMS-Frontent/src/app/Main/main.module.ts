import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FeatureModule } from "../Shared/Module/feature/feature.module";
import { MainRoutingModule } from './main-routing.module';
import { ProductFilterComponent } from './product-list/product-filter/product-filter.component';
import { CategoryProductListComponent } from './product-list/product-detail/category-product-list/category-product-list.component';
import { MainComponent } from './main.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { ProductListComponent } from './product-list/product-list.component';
import { HomeComponent } from './home/home.component';
import { CMSPageContentComponent } from './cmspage-content/cmspage-content.component';
import { UserAccountModule } from './user-account/user-account.module';

@NgModule({
  declarations: [HomeComponent,
    ProductListComponent,
    ProductDetailComponent,
    MainComponent,
    CategoryProductListComponent,
    ProductFilterComponent,
    CMSPageContentComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    FeatureModule,
    UserAccountModule
  ],
  exports: []
})
export class MainModule { }
