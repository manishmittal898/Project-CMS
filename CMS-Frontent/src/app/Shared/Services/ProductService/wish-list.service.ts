import { ProductMasterViewModel } from './product.service';
import { Injectable } from '@angular/core';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';

import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class WishListService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<WishListViewModel[]>> {
    let url = `${this._baseService.API_Url.ProductWishList_Api}`;
    return this._baseService.post(url, model);
  }

  AddProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Add_Apo}`;
    return this._baseService.post(url, model);
  }

  RemoveProduct(model: WishListPostModel): Observable<ApiResponse<WishListViewModel>> {
    let url = `${this._baseService.API_Url.ProductWishList_Remove_Apo}`;
    return this._baseService.post(url, model);
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
