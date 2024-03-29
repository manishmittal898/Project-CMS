import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HtmlComponent } from './Shared/Page/html/html.component';
import { LoginComponent } from './Shared/Page/login/login.component';
import { RegisterComponent } from './Shared/Page/register/register.component';
import { UIDesignModule } from './uidesign/uidesign.module';
import { SocialLoginCallbackComponent } from './Shared/Page/login/social-login-callback/social-login-callback.component';

const routes: Routes = [
  {
    path: "",
    loadChildren: () => import("./Main/main.module").then(m => m.MainModule)
  },
  {
    path: "b-to-b",
    loadChildren: () => import("./btob/btob.module").then(m => m.BtoBModule)
  },
  {
    path: "design",
    loadChildren: () => import("./uidesign/uidesign-routing.module").then(m => m.UIDesignRoutingModule)
  },
  {
    component: LoginComponent, path: 'login',
  },
  {
    component: LoginComponent, path: 'login/:returnURL',
  },
  {
    component: RegisterComponent, path: 'register',
  },
  {
    component: SocialLoginCallbackComponent, path: 'callback',
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
