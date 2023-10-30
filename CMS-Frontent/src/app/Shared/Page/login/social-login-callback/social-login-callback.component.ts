import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';
import { CartProductService } from 'src/app/Shared/Services/ProductService/cart-product.service';
import { WishListService } from 'src/app/Shared/Services/ProductService/wish-list.service';
import { AccountService } from 'src/app/Shared/Services/UserService/account.service';
import { AuthService, LoginUserDetailModel } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-social-login-callback',
  templateUrl: './social-login-callback.component.html',
  styleUrls: ['./social-login-callback.component.css']
})
export class SocialLoginCallbackComponent implements OnInit {

  constructor(private readonly _accountService: AccountService, private readonly _wishList: WishListService,
    private readonly _authService: AuthService, private readonly _cartService: CartProductService,
    private readonly _route: Router, private readonly toast: ToastrService, private readonly _activatedRoute: ActivatedRoute) {
    this._activatedRoute.queryParams.subscribe(p => {
      switch (p.client) {
        case "google":
          this.googleLogin();
          break;

        default:
          break;
      }

    })
  }

  ngOnInit(): void {
  }

  googleLogin() {
    let credential = sessionStorage.getItem('googleLoginUser');

    const responsePayload = decodeJWTToken(credential);
    let name = responsePayload.name.split(" ");
    let postModel = {
      Email: responsePayload.email,
      FirstName: name[0],
      LastName: name.length > 1 ? name[1] : null,
      ProfilePhoto: responsePayload.picture,
      Plateform: "Customer"

    }


    this._accountService.SocialLogin(postModel).subscribe((res) => {
      if (res.IsSuccess) {
        sessionStorage.removeItem('googleLoginUser')

        let data = res.Data as LoginUserDetailModel;
        this._authService.SaveUserToken(data.Token, true);
        this._authService.SaveUserDetail(data);
        this.toast.success(res.Message?.toString(), 'Login');
        this._authService.IsAuthenticate();

        if (this._wishList.wishListItem.length > 0) {
          this._wishList.syncWishList();
        }
        if (this._cartService.CartProductModel.length > 0) {
          this._cartService.syncCartProduct();
        }
        if (this._activatedRoute.snapshot.queryParams.returnURL) {
          this._route.navigate([this._activatedRoute.snapshot.queryParams.returnURL]);

        } else {
          this._route.navigate(['/user']);
        }
      } else {
        this._route.navigate(['/login']);

        this.toast.info(res.Message?.toString(), 'Login');
      }
    }, error => {
      this._route.navigate(['/login']);
      this.toast.info(error.message?.toString(), 'Login');
    });

    function decodeJWTToken(token) {
      return JSON.parse(atob(token.split(".")[1]));
    }
  }

}
