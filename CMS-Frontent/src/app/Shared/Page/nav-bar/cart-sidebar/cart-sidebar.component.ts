import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../../Services/Core/security.service';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { DropDownItem } from '../../../Helper/Common';
import { CartProductService, CartProductViewModel } from 'src/app/Shared/Services/ProductService/cart-product.service';

@Component({
  selector: 'app-cart-sidebar',
  templateUrl: './cart-sidebar.component.html',
  styleUrls: ['./cart-sidebar.component.css']
})
export class CartSidebarComponent implements OnInit {
  sizeModel: DropDownItem[];
  get cartModel(): CartProductViewModel[] {
    return this._cartService.CartProductModel;
  }
  constructor(private readonly _security: SecurityService, private readonly _commonService: CommonService, private _cartService: CartProductService) {
    this._cartService.GetCartList();
    // this.cartModel[0].ProductId
  }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    if (this._security.checkLocalStorage('cart-ddl-size', true)) {
      this.sizeModel = JSON.parse(this._security.getStorage('cart-ddl-size', true));

    } else {
      let serve = this._commonService.GetDropDown([DropDown_key.ddlProductSize], true).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          const ddls = res?.Data as DropDownModel;
          this.sizeModel = ddls.ddlProductSize;
          this._security.setStorage('cart-ddl-size', JSON.stringify(this.sizeModel), true)

        }
      });
    }
  }

  getSizeItem(sizeId, productId) {
    debugger
    let allSize = this.cartModel.filter(x => x.ProductId == productId).map(x => x.SizeId);
   return this.sizeModel.filter(x => x.Value == sizeId || !allSize.includes(x.Value))
  }
}
