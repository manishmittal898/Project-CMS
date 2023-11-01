import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Services/Core/common.service';
import { DropDownItem, DropDownModel, GroupDropDownItem } from '../../Helper/Common';
import { x64 } from 'crypto-js';
import { AuthService } from '../../Services/UserService/auth.service';
import { SecurityService } from '../../Services/Core/security.service';
import { DropDown_key } from '../../Constant';
import { CartProductService } from '../../Services/ProductService/cart-product.service';
import { Router } from '@angular/router';
import { BaseAPIService } from '../../Services/Core/base-api.service';
declare var $: any;
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})

export class NavBarComponent implements OnInit {
  isLoggedIn = false;
  menuModel: GroupDropDownItem[] = [];
  cmsPageMenu: DropDownItem[] = [];
  get cartCount() {
    return this._cartService?.CartProductModel?.length > 0 ? this._cartService?.CartProductModel?.length : 0;
  }
  // get wishListCount() {
  //   return this._cartService?.CartProductModel?.length > 0 ? this._cartService?.CartProductModel?.length : 0;
  // }
  constructor(private readonly _commonService: CommonService, private readonly _securityService: SecurityService, private readonly _baseService: BaseAPIService,
    private readonly _authService: AuthService, private readonly _cartService: CartProductService, private _router: Router) {
    if (this._securityService.getStorage('nav-collections-menu')) {
      this.menuModel = JSON.parse(this._securityService.getStorage('nav-collections-menu'));
    }
    if (this._securityService.getStorage('nav-cms-page-menu')) {
      this.cmsPageMenu = JSON.parse(this._securityService.getStorage('nav-cms-page-menu'));

    }
  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();
    this.GetDropDown();
    setTimeout(() => {
      this._authService.IsAuthentication.subscribe(x => {
        if (!x && this.isLoggedIn) {
          this.logout();
        }
        this.isLoggedIn = x as boolean ?? false;

      });
      this.MobileMenuToogle()
    }, 100);
  }

  GetDropDown() {
    let itms = [];
    // if (this.menuModel.length == 0) {
    itms.push(DropDown_key.ddlLookupGroup)
    // }
    // if (this.cmsPageMenu.length == 0) {
    itms.push(DropDown_key.ddlCMSPage)
    // }
    if (itms.length > 0) {

      const serve = this._commonService.GetDropDown(itms, true).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          const ddls = res?.Data as DropDownModel;
          this.menuModel = ddls.ddlLookupGroup?.map(x => {
            return {
              Category: x.Category, CategoryId: String(x.CategoryId),
              Data: x.Data.map(sd => { return { Text: sd.Text, Value: sd.Value, Category: sd.Category, CategoryId: sd.CategoryId } })
            } as any
          });
          this.cmsPageMenu = ddls?.ddlCMSPage?.map(x => { return { Text: x.Text, Value: x.Value } as DropDownItem });
          this._securityService.setStorage('nav-collections-menu', JSON.stringify(this.menuModel));
          this._securityService.setStorage('nav-cms-page-menu', JSON.stringify(this.cmsPageMenu));
        }
      });
    }
  }


  logout() {

    this._authService.LogOut();
    //  setTimeout(() => {
    if (this._router.url.includes('/user')) {
      this._router.navigate([this._baseService.Routing_Url.storeUrl]).then(() => {
        window.location.reload();
      });
    }
    else if (this._router.url !== this._baseService.Routing_Url.LoginUrl && !this._router.url.includes(this._baseService.Routing_Url.storeUrl)) {
      this._router.navigate([this._baseService.Routing_Url.LoginUrl]).then(() => {
        window.location.reload();
      });
    } else {
      window.location.reload();

    }
    // }, 10);
  }

  MobileMenuToogle() {
    $("#ChangeToggle").click(function () {
      $("#MenuNavbar").addClass("menushowing");
    });
    $(".mobile-nav-close").click(function () {
      $("#MenuNavbar").removeClass("menushowing");
    });
  }
}
