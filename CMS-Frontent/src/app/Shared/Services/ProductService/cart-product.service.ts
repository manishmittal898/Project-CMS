import { ProductMasterViewModel } from './product.service';
import { AuthService } from './../UserService/auth.service';
import { ToastrService } from 'ngx-toastr';
import { BaseAPIService } from './../Core/base-api.service';
import { Injectable } from '@angular/core';
import { Observable, forkJoin } from 'rxjs';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { SecurityService } from '../Core/security.service';

@Injectable({
  providedIn: 'root'
})
export class CartProductService {

  cartProductItem: any[] = [];
  constructor(private readonly _baseService: BaseAPIService, private _toasterService: ToastrService, private _auth: AuthService, private _securityService: SecurityService) {
    this.cartProductItem = this._securityService.checkLocalStorage('cart-product') ? JSON.parse(this._securityService.getStorage('cart-product') as string) as any[] : [];
  }

  GetList(model: IndexModel): Observable<ApiResponse<ProductMasterViewModel[]>> {
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

  public async SetCartProduct(product: ProductMasterViewModel) {
    let model = { ProductId: product.Id } as CartProductPostModel;
    if (this._auth.IsAuthentication.value) {
      if (product.IsWhishList) {
        this.RemoveProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            product.IsWhishList = !product.IsWhishList;
            this._toasterService.success(x.Message as string, 'Success');
            this.cartProductItem.splice(this.cartProductItem.indexOf(product.Id), 1);

          } else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          let data = JSON.stringify(this.cartProductItem);
          this._securityService.setStorage('wishlist', data);
          return x;
        }, error => {
          this._toasterService.error(error.message as string, 'Failed');
        })
      } else {
        this.AddProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            product.IsWhishList = !product.IsWhishList;
            this.cartProductItem.push(product.Id);
            this._toasterService.success(x.Message as string, 'Success');
          }
          else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          let data = JSON.stringify(this.cartProductItem);
          this._securityService.setStorage('wishlist', data);
          return x;
        },
          error => {
            this._toasterService.error(error.message as string, 'Failed');
          })
      }
    } else {
      if (this.cartProductItem.indexOf(product.Id) == -1) {
        this.cartProductItem.push(product.Id);
        this._toasterService.success("Added Successfully" as string, 'Success');
      } else {
        this.cartProductItem.splice(this.cartProductItem.indexOf(product.Id), 1);
        this._toasterService.success("Removed successfully" as string, 'Success');
      }
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
      product.IsWhishList = !product.IsWhishList;
    }
  }


  public async syncCartProduct() {
    let sub = [] as any[];
    this.cartProductItem.forEach(x => {
      sub.push(this.AddProduct({ ProductId: x }))
    })
    forkJoin(sub).subscribe(res => {
      this.cartProductItem = [];
      this._securityService.deleteStorage('cart-product');
    })
  }


}

export interface CartProductViewModel {
  Id: string;
  ProductId: string;
  AddedOn: string;
  Product: ProductMasterViewModel;
}

export interface CartProductPostModel {
  ProductId: string;
}
