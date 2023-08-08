import { ProductMasterViewModel } from './product.service';
import { Injectable } from '@angular/core';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';

import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../UserService/auth.service';
@Injectable({
  providedIn: 'root'
})
export class WishListService {
  wishListItem: ProductMasterViewModel[] = [];
  constructor(private readonly _baseService: BaseAPIService, private _toasterService: ToastrService, private _auth: AuthService) { }

  GetList(model: IndexModel): Observable<ApiResponse<ProductMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.ProductWishList_Api}`;
    return this._baseService.post(url, model);
  }

  private AddProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Add_Apo}`;
    return this._baseService.post(url, model);
  }

  private RemoveProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Remove_Apo}`;
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
          } else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          return x;
        }, error => {
          this._toasterService.error(error.message as string, 'Failed');
        })
      } else {
        this.AddProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            product.IsWhishList = !product.IsWhishList;
            this._toasterService.success(x.Message as string, 'Success');
          }
          else {
            this._toasterService.error(x.Message as string, 'Faild');
          }
          return x;
        },
          error => {
            this._toasterService.error(error.message as string, 'Failed');
          })
      }
    } else {
      if (this.wishListItem.indexOf(product) == -1) {
        this.wishListItem.push(product);
        this._toasterService.success("Added Successfully" as string, 'Success');
      } else {
        this.wishListItem.splice(this.wishListItem.indexOf(product), 1);
        this._toasterService.success("Removed successfully" as string, 'Success');
      }
      product.IsWhishList = !product.IsWhishList;
    }

  }

}

export interface WishListViewModel {
  Id: number;
  ProductId: number;
  AddedOn: string;
  Product: ProductMasterViewModel;
}

export interface WishListPostModel {
  ProductId: number;
}
