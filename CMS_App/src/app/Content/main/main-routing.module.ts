import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HtmlComponent } from '../html/html.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { MainComponent } from './main.component';

const routes: Routes = [

  {
    path: '',
    component: MainComponent,
    children: [{
      component: AdminDashboardComponent, path: ''
    },
    {
      component: AdminDashboardComponent, path: 'admin'
    },
    { path: "product", loadChildren: () => import('./product/product.module').then(m => m.ProductModule) },
    { path: "master", loadChildren: () => import('./master/master.module').then(m => m.MasterModule) },
    { path: "user", loadChildren: () => import('./user/user.module').then(m => m.UserModule) },

    { path: 'manish', component: HtmlComponent },
    ]
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
