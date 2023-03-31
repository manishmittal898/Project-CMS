import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products.component';
import { TryForFreeComponent } from './try-for-free/try-for-free.component';

const routes: Routes = [
  {
    path: '',
    component: ProductsComponent,
    children: [
      { component: TryForFreeComponent, path: '' },
      { component: TryForFreeComponent, path: 'free-demo' },

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
