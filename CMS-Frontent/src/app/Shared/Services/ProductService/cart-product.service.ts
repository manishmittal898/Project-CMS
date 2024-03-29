import { ProductFilterModel, ProductMasterViewModel, ProductService } from './product.service';
import { AuthService } from './../UserService/auth.service';
import { ToastrService } from 'ngx-toastr';
import { BaseAPIService } from './../Core/base-api.service';
import { Injectable } from '@angular/core';
import { Observable, forkJoin } from 'rxjs';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { SecurityService } from '../Core/security.service';

@Injectable({
  providedIn: "root"
})
export class CartProductService {

  private cartProductItem: CartProductPostModel[] = [];
  CartProductModel?: CartProductViewModel[] = [];
  get TotalAmount() {
    let amt = 0;
    if (this.CartProductModel.length > 0) {
      this.CartProductModel?.forEach(x => {
        amt += (x.Quantity ?? 0) * (x.Product?.Stocks?.find(c => c.SizeId == x.SizeId)?.SellingPrice ?? 0)
      });
    }

    return amt;
  }
  get TotalMRP() {
    let amt = 0;
    if (this.CartProductModel.length > 0) {
      this.CartProductModel?.forEach(x => {
        amt += (x.Quantity ?? 0) * (x.Product?.Stocks?.find(c => c.SizeId == x.SizeId)?.UnitPrice ?? 0)
      });
    }

    return amt;
  }

  constructor(private readonly _baseService: BaseAPIService, private _toasterService: ToastrService,
    private _auth: AuthService, private _securityService: SecurityService, private readonly _productService: ProductService) {
    this.cartProductItem = this._securityService.checkStorage('cart-product') ? JSON.parse(this._securityService.getStorage('cart-product') as string) as any[] : [];
    _auth.IsAuthentication.subscribe(res => {

      this.GetCartList();
    })

  }

  private GetList(model: IndexModel): Observable<ApiResponse<CartProductViewModel[]>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Api}`;
    return this._baseService.post(url, model);
  }

  private AddProduct(model: CartProductPostModel): Observable<ApiResponse<CartProductViewModel>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Add_Api}`;
    return this._baseService.post(url, model);
  }

  private RemoveProduct(model: CartProductPostModel): Observable<ApiResponse<CartProductViewModel>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Remove_Api}`;
    return this._baseService.post(url, model);
  }

  private UpdateCartQuantity(model: UpdateCartItemPostModel) {
    let url = `${this._baseService.API_Url.UserCartProduct_Update_Api}`;
    return this._baseService.post(url, model);
  }
  public async SetCartProduct(product: CartProductPostModel) {
    var indx = this.cartProductItem.findIndex(x => x.ProductId == product.ProductId && x.SizeId == product.SizeId);
    if (this._auth.IsAuthentication.value) {
      this.AddProduct(product).subscribe(x => {
        if (x.IsSuccess) {
          // if (indx == -1) {
          //   this.cartProductItem.push(product);
          // }
          this._toasterService.success(x.Message as string, 'Success');
        }
        else {
          this._toasterService.error(x.Message as string, 'Faild');
        }
        const data = JSON.stringify(this.cartProductItem);
        this._securityService.setStorage('cart-product', data);
        this.GetCartList();
        return x;
      },
        error => {
          this._toasterService.error(error.message as string, 'Failed');
        })

    } else {
      let showAddMessage = true
      if (indx == -1) {
        this.cartProductItem.push(product);
      } else {
        const tempProduct = this.CartProductModel?.find(x => x.ProductId == product.ProductId);
        const stockSize = tempProduct?.Product?.Stocks?.find(s => s.SizeId == product.SizeId)
        if (stockSize.Quantity < (this.cartProductItem[indx]?.Quantity + product.Quantity)) {
          this.cartProductItem[indx].Quantity = stockSize.Quantity;
          this._toasterService.warning("Maximum cart limit exceeded!" as string, 'info');
          showAddMessage = false;
        }
        else {
          this.cartProductItem[indx].Quantity += product.Quantity;
        }
      }
      if (showAddMessage) {
        this._toasterService.success("Added Successfully" as string, 'Success');
      }
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
      this.GetCartList();
    }
  }

  public async UpdateCartProduct(product: CartProductViewModel) {
    if (this._auth.IsAuthentication.value) {
      let model = {} as UpdateCartItemPostModel;
      model.Id = product.Id;
      model.Quantity = product.Quantity;
      this.UpdateCartQuantity(model).toPromise().then(x => {
        return x.IsSuccess;
      })
    }
    else {
      let ItemData = this.CartProductModel.map(x => { return { ProductId: x.ProductId, Quantity: x.Quantity, SizeId: x.SizeId } as CartProductPostModel })
      this.cartProductItem = ItemData;
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
    }
  }
  public async GetCartList() {
    if (!this._auth.IsAuthentication.value) {
      let data = this.cartProductItem.map(x => x.ProductId);
      let indexModel = new ProductFilterModel();
      indexModel.Ids = data;
      indexModel.PageSize = 101;
      if (data?.length > 0) {
        let calls: any = {};
        this.cartProductItem.forEach(x => {
          calls[x.ProductId] = this._productService.GetDetail(x.ProductId, true);
        })
        forkJoin(calls).subscribe(res => {
          this.CartProductModel = [];
          this.cartProductItem.forEach(element => {
            let response = res[element.ProductId] as ApiResponse<ProductMasterViewModel>
            if (response.IsSuccess) {
              let itm = {} as CartProductViewModel;
              itm.Product = response.Data;
              itm.Id = '';
              itm.Quantity = element.Quantity;
              itm.SizeId = element.SizeId;
              itm.ProductId = element.ProductId;
              this.CartProductModel.push(itm);
            }
          });
        })

      } else {
        this.CartProductModel = [];
      }

    } else {
      let indexModel = new IndexModel();
      indexModel.PageSize = 101;
      this.GetList(indexModel).subscribe(response => {
        if (response.IsSuccess) {
          this.CartProductModel = response.Data;

        }
      })
    }
  }

  public async syncCartProduct() {
    let sub = [] as any[];
    this.CartProductModel.forEach(x => {
      sub.push(this.AddProduct({
        ProductId: x.ProductId,
        Quantity: x.Quantity,
        SizeId: x.SizeId
      } as CartProductPostModel))
    })
    forkJoin(sub).subscribe(res => {
      this.cartProductItem = [];
      this._securityService.removeStorage('cart-product');
      this.GetCartList();
    })
  }

  public async deleteProduct(productId, sizeId) {
    if (this._auth.IsAuthentication.value) {
      let indx = this.CartProductModel.findIndex(x => x.ProductId == productId && x.SizeId == sizeId)
      let model = {} as CartProductPostModel;
      model.ProductId = this.CartProductModel[indx].ProductId;
      model.SizeId = this.CartProductModel[indx].SizeId;
      model.Quantity = this.CartProductModel[indx].Quantity;
      this.RemoveProduct(model).toPromise().then(x => {
        if (x.IsSuccess) {

          this.CartProductModel.splice(indx, 1);
          let idx = this.cartProductItem?.findIndex(x => x.ProductId == productId);
          if (idx != -1) {
            this.cartProductItem.splice(idx, 1);
          }
          let data = JSON.stringify(this.cartProductItem);
          this._securityService.setStorage('cart-product', data);
          // this.GetCartList();
          //let idx = this.CartProductModel.findIndex(s => s.ProductId == productId && s.SizeId == sizeId);
        }
        return x.IsSuccess;
      })


    } else {
      let indx = this.cartProductItem.findIndex(x => x.ProductId == productId && x.SizeId == sizeId)
      this.cartProductItem.splice(indx, 1);
      let idx = this.CartProductModel.findIndex(s => s.ProductId == productId && s.SizeId == sizeId);
      this.CartProductModel.splice(idx, 1);
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);

      // this.GetCartList();
      return true;
    }

  }



}

export interface CartProductViewModel {
  Id: string;
  ProductId: string;
  SizeId: string;
  Size: string;
  Quantity: number;
  AddedOn: string;
  Product: ProductMasterViewModel;
}

export interface CartProductPostModel {
  ProductId: string;
  SizeId: string;
  Quantity: number;


}

export interface UpdateCartItemPostModel {
  Id: string;
  Quantity: number;
}
