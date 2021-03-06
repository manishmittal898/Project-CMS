import { NgModule } from '@angular/core';
import { Routes, RouterModule, NoPreloading } from '@angular/router';
import { LoginComponent } from './Content/Common/login/login.component';
import { PageNotFoundComponent } from './Content/Common/page-not-found/page-not-found.component';
import { HtmlComponent } from './Content/html/html.component';
import { AuthenticationGuard } from './Shared/Helper/authentication.guard';
import { Routing_Url } from './Shared/Helper/constants';


const routes: Routes = [
  { path: '', redirectTo: 'admin', pathMatch: 'full'},
  { path: "admin", loadChildren: () => import('./Content/main/main.module').then(m => m.MainModule) },


  { path: 'login' , component: LoginComponent },
  { path: '**', component: PageNotFoundComponent, canActivate: [AuthenticationGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: NoPreloading })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
