import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { MainPageComponent } from './main-page.component';

const routes: Routes = [
  {
    path: '',
    component: MainPageComponent,
    children: [{
      component: HomeComponent, path: ''
    },
    {
      component: HomeComponent, path: 'home'
    },
    {
      component: ProductListComponent, path: 'product'
    },
    {
      component: ProductDetailComponent, path: 'product/:id'
    },
    {
      component: ProductDetailComponent, path: 'product/:name/:id'
    }

  ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HtmlPagesRoutingModule { }
