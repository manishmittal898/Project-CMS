import { UIDesignComponent } from './uidesign.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path: '',
    component: UIDesignComponent,
    children: [{
      component: LoginComponent, path: '',

    },
    {
      component: LoginComponent, path: 'login',

    },
    {
      component: LoginComponent, path: 'register',

    }

    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UIDesignRoutingModule { }
