import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { SharedModule } from "src/app/Shared/Helper/shared/shared.module";
import { AdminDashboardComponent } from "./admin-dashboard/admin-dashboard.component";
import { MainRoutingModule } from "./main-routing.module";
import { MainComponent } from './main.component';



@NgModule({
  declarations: [

    AdminDashboardComponent,
    MainComponent
  ],
  imports: [
    CommonModule,
    
    MainRoutingModule,
    SharedModule
  ]
})
export class MainModule { }
