import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../../Services/Core/security.service';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { DropDown_key, Message } from 'src/app/Shared/Constant';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { DropDownItem } from '../../../Helper/Common';
import { CartProductService, CartProductViewModel } from 'src/app/Shared/Services/ProductService/cart-product.service';
import { ToastrService } from 'ngx-toastr';

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
  get TotalAmount() {
    let amt = 0;
    this.cartModel.forEach(x => {
      amt += x.Quantity * x.Product.SellingPrice
    })
    return amt;
  }
  constructor(private readonly _security: SecurityService, private _toasterService: ToastrService,
    private readonly _commonService: CommonService, private _cartService: CartProductService) {
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
    let allSize = this.cartModel.filter(x => x.ProductId == productId).map(x => x.SizeId);
    return this.sizeModel.filter(x => x.Value == sizeId || !allSize.includes(x.Value))
  }
  deleteCartItem(item: CartProductViewModel) {
    this._commonService.Question(Message.DeleteCartItem).then(result => {
      if (result) {
        this._cartService.deleteProduct(item.ProductId, item.SizeId).then(x => {
          this._toasterService.success("Cart item removed..!" as string, 'Removed');

        })
      }
    }, err => {
      this._toasterService.error(err.message as string, 'Oops');

    })
  }
}
