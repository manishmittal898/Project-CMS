import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../../../Services/Core/security.service';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { DropDown_key, Message } from 'src/app/Shared/Constant';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { DropDownItem } from '../../../Helper/Common';
import { CartProductPostModel, CartProductService, CartProductViewModel } from 'src/app/Shared/Services/ProductService/cart-product.service';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/Shared/Services/ProductService/product.service';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-cart-sidebar',
  templateUrl: './cart-sidebar.component.html',
  styleUrls: ['./cart-sidebar.component.css']
})
export class CartSidebarComponent implements OnInit {
  sizeModel: DropDownItem[];
  get cartModel(): CartProductViewModel[] {
    return this._cartService.CartProductModel ?? [];
  }
  get TotalAmount() {
    return this._cartService.TotalAmount;
  }
  get TotalMRP() {
    return this._cartService.TotalMRP;
  }
  constructor(private readonly _security: SecurityService, private _toasterService: ToastrService, private _route: Router,
    private readonly _commonService: CommonService, public _cartService: CartProductService, private readonly _productService: ProductService) {
    this._cartService.GetCartList();
    // this.cartModel[0].ProductId

  }


  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    if (this._security.checkStorage('cart-ddl-size', true)) {
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

  checkSizeExist(sizeId, productId, itm): boolean {
    let prdSize = this.cartModel?.find(x => x.ProductId == productId)?.Product?.Stocks?.map(x => x.SizeId) ?? [];
    let allSize = this.cartModel?.filter(x => x.ProductId == productId)?.map(x => x.SizeId);
    return this.sizeModel?.filter(x => x.Value == itm && prdSize?.includes(x.Value) && (x.Value == sizeId || !allSize?.includes(x.Value))).length > 0;
  }
  getSellingPrice(SizeId, ProductId) {
    let product = this.cartModel.find(x => x.ProductId == ProductId)
    return product.Product.Stocks.find(x => x.SizeId == SizeId).SellingPrice;
  }
  getMRPrice(SizeId, ProductId) {
    let product = this.cartModel.find(x => x.ProductId == ProductId)
    return product.Product.Stocks.find(x => x.SizeId == SizeId).UnitPrice;
  }
  isAvailableInStock(SizeId, ProductId) {
    let product = this.cartModel.find(x => x.ProductId == ProductId)
    return product.Product.Stocks.find(x => x.SizeId == SizeId).Quantity <= product.Quantity;

  }
  getMaxCartQuantity(SizeId, ProductId) {
    let product = this.cartModel.find(x => x.ProductId == ProductId)
    return product?.Product?.Stocks?.find(x => x.SizeId == SizeId)?.Quantity ?? 20;
  }

  deleteCartItem(item: CartProductViewModel) {
    this._commonService.Question(Message.DeleteCartItem).then(result => {
      if (result) {
        this._cartService.deleteProduct(item.ProductId, item.SizeId).then(x => {
          this._toasterService.success("Cart item removed..!" as string, 'Removed');
          this.UpdateCartProduct(item);
        })
      }
    }, err => {
      this._toasterService.error(err.message as string, 'Oops');

    })
  }
  UpdateCartProduct(product: CartProductViewModel) {
    this._cartService.UpdateCartProduct(product).then(x => {

    })
  }
  getUpdatedPrice(SizeId, ProductId) {

    this._productService.GetStockDetail(ProductId, SizeId).subscribe(res => {
      if (res.IsSuccess) {
        let itmIndex = this._cartService.CartProductModel.findIndex(x => x.ProductId == ProductId && x.SizeId == SizeId);
        let sizeIndex = this._cartService.CartProductModel[itmIndex].Product.Stocks.findIndex(x => x.SizeId == SizeId);
        this._cartService.CartProductModel[itmIndex].Product.Stocks[sizeIndex].SellingPrice = res.Data.SellingPrice;
        this._cartService.CartProductModel[itmIndex].Product.Stocks[sizeIndex].UnitPrice = res.Data.UnitPrice;
        this._cartService.CartProductModel[itmIndex].Quantity =
          this._cartService.CartProductModel[itmIndex].Quantity > res.Data.Quantity ?
            res.Data.Quantity : this._cartService.CartProductModel[itmIndex].Quantity;

      }
    })
  }
  getDetailUrl(Product) {
    return `/collections/${Product.Category?.replace('/', '-').split(' ').join('-')}/${Product.Name.replace('/', '-').split(' ').join('-')}/${Product.Id}`
  }
  redirectToPage() {
    debugger
    if (this._route.url.includes('/shop/cart')) {
      history.back();
    }
  }
}
