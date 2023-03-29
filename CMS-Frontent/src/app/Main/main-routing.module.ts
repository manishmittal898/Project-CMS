import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductDetailComponent } from './product-list/product-detail/product-detail.component';
import { MainComponent } from './main.component';
import { CMSPageContentComponent } from './cmspage-content/cmspage-content.component';
import { UserAccountModule } from './user-account/user-account.module';

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
      component: ProductListComponent, path: 'store/:name'
    },
    {
      component: ProductDetailComponent, path: 'store/:id'
    },
    {
      component: ProductDetailComponent, path: 'store/:name/:id'
    },
    {
      component: CMSPageContentComponent, path: 'page/:name'
    },
    {
      path: "user",
      loadChildren: () => import("./user-account/user-account.module").then(m => m.UserAccountModule)
    },

    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
