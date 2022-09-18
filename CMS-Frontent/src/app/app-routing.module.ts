import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { UIDesignModule } from './uidesign/uidesign.module';

const routes: Routes = [
  {
    path: "",
    loadChildren: () => import("./Main/main.module").then(m => m.MainModule)
  },
  {
    path: "design",
    loadChildren: () => import("./uidesign/uidesign-routing.module").then(m => m.UIDesignRoutingModule)
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
