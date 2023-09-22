import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { MainComponent } from './main.component';
import { CMSPageContentComponent } from './cmspage-content/cmspage-content.component';
import { UserAccountModule } from './user-account/user-account.module';
import { ShopModule } from './shop/shop.module';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [{
      component: HomeComponent, path: ''
    },
    {
      component: HomeComponent, path: 'home'
    },
    {
      component: ProductListComponent, path: 'store'
    },
    {
      component: ProductListComponent, path: 'collections/:name'
    },
    {
      component: ProductDetailComponent, path: 'collections/:id'
    },
    {
      component: ProductDetailComponent, path: 'collections/:name/:id'
    },
    {
      component: ProductDetailComponent, path: 'collections/:category/:name/:id'
    },
    {
      component: CMSPageContentComponent, path: 'page/:name'
    },
    {
      path: "user",
      loadChildren: () => import("./user-account/user-account.module").then(m => m.UserAccountModule)
    },
    {
      path: "shop",
      loadChildren: () => import("./shop/shop.module").then(m => m.ShopModule)
    },

    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
