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
    { path: 'manish', component: HtmlComponent },
    ]
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
