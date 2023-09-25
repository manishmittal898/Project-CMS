import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthService, LoginUserDetailModel } from '../../Services/UserService/auth.service';
import { Routing_Url } from '../../Constant';
import { AccountService } from '../../Services/UserService/account.service';
import { SecurityService } from '../../Services/Core/security.service';
import { WishListService } from '../../Services/ProductService/wish-list.service';
import { CartProductService } from '../../Services/ProductService/cart-product.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isShowPass = false;
  model: any = {};
  get routing_Url() { return Routing_Url };

  constructor(private readonly _accountService: AccountService, private readonly _wishList: WishListService,
    private readonly _authService: AuthService, private readonly _security: SecurityService,private readonly _cartService : CartProductService,
    private readonly _route: Router, private readonly toast: ToastrService, private readonly _activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onSubmit() {

    this.model.Plateform = "Customer";
    if (this.model.Email == undefined || this.model.Password == undefined) {
      this.toast.warning('Please enter username and password', 'Required');
      return;
    }
    if (!environment.IsAutoLogin) {
      this._security.GetEncryptedText(this.model.Password).then(x => {
        if (x.IsSuccess) {
          const encPass = x.Data;
          const postModel = {
            Email: this.model.Email,
            Password: encPass,
            Plateform: this.model.Plateform
          };
          this._accountService.Login(postModel).subscribe((res) => {
            if (res.IsSuccess) {

              let data = res.Data as LoginUserDetailModel;
              this._authService.SaveUserToken(data.Token);
              this._authService.SaveUserDetail(data);
              this.toast.success(res.Message?.toString(), 'Login');
              this._authService.IsAuthenticate();

              if (this._wishList.wishListItem.length > 0) {
                this._wishList.syncWishList();
              }
              if (this._wishList.wishListItem.length > 0) {
                this._cartService.syncCartProduct();
              }
              if (this._activatedRoute.snapshot.queryParams.returnURL) {
                this._route.navigate([this._activatedRoute.snapshot.queryParams.returnURL]);

              } else {
                this._route.navigate(['/user']);
              }
            } else {
              this.toast.info(res.Message?.toString(), 'Login');
            }
          });
        }
      });
    } else {
      this._authService.SaveUserToken("testtoken");
      this._route.navigate(['']);
    }
  }

  forgetPassword() { }
}
