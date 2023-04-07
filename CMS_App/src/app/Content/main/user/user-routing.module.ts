import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';


const routes: Routes = [


    { path: '', redirectTo: 'list', pathMatch: 'full' },
    { path: 'list', component: UserComponent },
    // { path: 'add', component: UserAddEditComponent },
    // { path: 'edit/:id', component: UserAddEditComponent },
    // { path: 'detail/:id', component: UserDetailComponent },












    
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
