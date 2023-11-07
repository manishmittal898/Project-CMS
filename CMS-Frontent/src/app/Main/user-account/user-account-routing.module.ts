import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { MyAccountComponent } from "./my-account/my-account.component";
import { MyAddressComponent } from "./my-address/my-address.component";
import { MyOrdersComponent } from "./my-orders/my-orders.component";
import { MyWishlistComponent } from "./my-wishlist/my-wishlist.component";
import { UserAccountComponent } from "./user-account.component";
import { AuthGuard } from "src/app/Shared/Services/Core/auth.guard";

const routes: Routes = [
  {
    path: '',
    component: UserAccountComponent,
    children: [
      { component: MyAccountComponent, path: '' },
      { component: MyAccountComponent, path: 'profile', canActivate: [AuthGuard] },
      { component: MyOrdersComponent, path: 'orders', canActivate: [AuthGuard] },
      { component: MyAddressComponent, path: 'address', canActivate: [AuthGuard] },
      { component: MyWishlistComponent, path: 'wishlist' }
    ]

  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserAccountRoutingModule { }
