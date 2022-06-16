import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products.component';
import { ProductAddEditComponent } from './product-add-edit/product-add-edit.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';

const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full' },
  { path: 'list', component: ProductsComponent },
  { path: 'add', component: ProductAddEditComponent },
  { path: 'edit/:id', component: ProductAddEditComponent },
  { path: 'detail/:id', component: ProductDetailComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
