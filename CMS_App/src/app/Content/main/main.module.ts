import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "src/app/Shared/Helper/shared/shared.module";
import { AdminDashboardComponent } from "./admin-dashboard/admin-dashboard.component";
import { MainRoutingModule } from "./main-routing.module";
import { MainComponent } from './main.component';
import { ProductModule } from "./product/product.module";
import { MasterModule } from './master/master.module';
import { UserModule } from './user/user.module';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    MainComponent
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    ProductModule,
    MasterModule,
    UserModule,
    SharedModule
  ],
  bootstrap: [MainComponent]
})
export class MainModule { }
