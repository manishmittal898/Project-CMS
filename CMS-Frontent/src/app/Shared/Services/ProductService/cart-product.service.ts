import { ProductMasterViewModel } from './product.service';
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

  cartProductItem: CartProductPostModel[] = [];
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

  public async SetCartProduct(product: CartProductPostModel) {
    //  let model = { ProductId: product.Id } as CartProductPostModel;
    var indx = this.cartProductItem.findIndex(x => x.ProductId == product.ProductId && x.SizeId == product.SizeId);
    if (indx >= 0) {
      product = Object.assign([], this.cartProductItem[indx]);
      product.Quantity++;
    }
    if (this._auth.IsAuthentication.value) {

      this.AddProduct(product).subscribe(x => {
        if (x.IsSuccess) {
          if (indx >= 0) {
            this.cartProductItem[indx] = product;
          } else {
            this.cartProductItem.push(product);
          }
          this._toasterService.success(x.Message as string, 'Success');
        }
        else {
          this._toasterService.error(x.Message as string, 'Faild');
        }
        let data = JSON.stringify(this.cartProductItem);
        this._securityService.setStorage('cart-product', data);
        return x;
      },
        error => {
          this._toasterService.error(error.message as string, 'Failed');
        })

    } else {
      if (indx >= 0) {
        this.cartProductItem[indx] = product;
      } else {
        this.cartProductItem.push(product);
      }
      this._toasterService.success("Added Successfully" as string, 'Success');
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
    }
  }


  public async syncCartProduct() {
    let sub = [] as any[];
    this.cartProductItem.forEach(x => {
      sub.push(this.AddProduct(x))
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
