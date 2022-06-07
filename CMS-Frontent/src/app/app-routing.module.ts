import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  {
    path: "html",
    loadChildren: () => import("./Pages/html-pages/html-pages.module").then(m => m.HtmlPagesModule)
  },
  { path: '', redirectTo: 'html', pathMatch: 'full' },
  { path: '**', redirectTo: 'html' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
