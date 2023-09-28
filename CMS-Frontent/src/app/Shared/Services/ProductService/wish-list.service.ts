import { ProductMasterViewModel } from './product.service';
import { Injectable } from '@angular/core';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';

import { Observable, forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../UserService/auth.service';
import { SecurityService } from '../Core/security.service';
@Injectable({
  providedIn: 'root'
})
export class WishListService {
  wishListItem: any[] = [];
  constructor(private readonly _baseService: BaseAPIService, private _toasterService: ToastrService, private _auth: AuthService, private _securityService: SecurityService) {
    this.wishListItem = this._securityService.checkStorage('wishlist') ? JSON.parse(this._securityService.getStorage('wishlist')) as any[] : [];
  }

  GetList(model: IndexModel): Observable<ApiResponse<ProductMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.ProductWishList_Api}`;
    return this._baseService.post(url, model);
  }

  private AddProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Add_Api}`;
    return this._baseService.post(url, model);
  }

  private RemoveProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Remove_Api}`;
    return this._baseService.post(url, model);
  }

  public async SetWishlistProduct(product: ProductMasterViewModel) {
    let model = { ProductId: product.Id } as WishListPostModel;
    if (this._auth.IsAuthentication.value) {
      if (product.IsWhishList) {
        this.RemoveProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            product.IsWhishList = !product.IsWhishList;
            this._toasterService.success(x.Message as string, 'Success');
            this.wishListItem.splice(this.wishListItem.indexOf(product.Id), 1);

          } else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          let data = JSON.stringify(this.wishListItem);
          this._securityService.setStorage('wishlist', data);
          return x;
        }, error => {
          this._toasterService.error(error.message as string, 'Failed');
        })
      } else {
        this.AddProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            product.IsWhishList = !product.IsWhishList;
            this.wishListItem.push(product.Id);
            this._toasterService.success(x.Message as string, 'Success');
          }
          else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          let data = JSON.stringify(this.wishListItem);
          this._securityService.setStorage('wishlist', data);
          return x;
        },
          error => {
            this._toasterService.error(error.message as string, 'Failed');
          })
      }
    } else {
      if (this.wishListItem.indexOf(product.Id) == -1) {
        this.wishListItem.push(product.Id);
        this._toasterService.success("Added Successfully" as string, 'Success');
      } else {
        this.wishListItem.splice(this.wishListItem.indexOf(product.Id), 1);
        this._toasterService.success("Removed successfully" as string, 'Success');
      }
      let data = JSON.stringify(this.wishListItem);
      this._securityService.setStorage('wishlist', data);
      product.IsWhishList = !product.IsWhishList;
    }
  }

  public async syncWishList() {
    let sub = [];
    this.wishListItem.forEach(x => {
      sub.push(this.AddProduct({ ProductId: x }))
    })
    forkJoin(sub).subscribe(res => {
      this.wishListItem = [];
      this._securityService.removeStorage('wishlist');
    })
  }

}

export interface WishListViewModel {
  Id: string;
  ProductId: string;
  AddedOn: string;
  Product: ProductMasterViewModel;
}

export interface WishListPostModel {
  ProductId: string;
}
