import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IndexModel, ApiResponse } from '../Helper/Common';
import { BaseAPIService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<ProductCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.Product_List_Api}`;
    return this._baseService.post(url, model);
  }


  GetCategoryProduct(model: IndexModel): Observable<ApiResponse<ProductCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.Product_Category_Api}`;
    return this._baseService.post(url, model);
  }



}

export interface ProductMasterViewModel {
  Id: number;
  Name: string;
  ImagePath: string;
  CategoryId: number;
  SubCategoryId: number;
  Desc: string;
  Price: number | null;
  CaptionTagId: number | null;
  Summary: string;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;
  IsActive: boolean | null;
  IsDelete: boolean;
  CaptionTag: string;
  Category: string;
  SubCategory: string;
  Files: ProductImageViewModel[];
}

export interface ProductImageViewModel {
  Id: number;
  FilePath: string;
  ProductId: number | null;
}


export interface ProductCategoryViewModel {
  Id: number;
  Name: string;
  ImagePath: string;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;
  IsActive: boolean | null;
  IsDelete: boolean;
}
