import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BtoBComponent } from "./btob.component";

const routes: Routes = [
  {
    path: '',
    component: BtoBComponent,
    children: [{
      component: BtoBComponent, path: ''
    },

    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BtoBRoutingModule { }
