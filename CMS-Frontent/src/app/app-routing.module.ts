import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HtmlComponent } from './Shared/Page/html/html.component';

const routes: Routes = [
  {
    path: "",
    loadChildren: () => import("./Pages/html-pages/html-pages.module").then(m => m.HtmlPagesModule)
  },
  { path: 'html-page', component: HtmlComponent },
  { path: '', redirectTo: '', pathMatch: 'full' },
  { path: '**', redirectTo: 'index' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
