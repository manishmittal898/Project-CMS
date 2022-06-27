import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductsComponent } from './products.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductAddEditComponent } from './product-add-edit/product-add-edit.component';
import { ProductGalleryComponent } from './product-add-edit/product-gallery/product-gallery.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';


@NgModule({
  declarations: [
    ProductsComponent,
    ProductDetailComponent,
    ProductAddEditComponent,
    ProductGalleryComponent
  ],
  imports: [
    CommonModule,
    ProductRoutingModule,
    SharedModule
  ]
})
export class ProductModule { }
