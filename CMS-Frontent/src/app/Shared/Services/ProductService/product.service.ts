import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';
@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<ProductMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.Product_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetDetail(Id: string): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_Detail_Api}`;
    return this._baseService.get(`${url + Id}`);
  }

  GetCategoryProduct(model: IndexModel): Observable<ApiResponse<ProductCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.Product_Category_Api}`;
    return this._baseService.post(url, model);
  }
}

export class ProductFilterModel extends IndexModel {
  constructor() {
    super();
    this.PageSize = 40;
    this.CategoryId = [];
    this.SubCategoryId = [];
    this.SizeId = [];
    this.Price = [50, 100000];
    this.ViewSectionId = [];
  }
  CategoryId!: number[];
  SubCategoryId!: number[];
  Price!: number[];
  IsAvailableStock!: boolean;
  SizeId!: number[];
  Keyword!: string;
  ViewSectionId!: number[];
  Ids!: number[]
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
  CaptionTag: string;
  Category: string;
  SubCategory: string;
  ShippingCharge: number | undefined;
  IsWhishList: boolean;
  ViewSectionId: number | null;
  Files: ProductImageViewModel[];
  Stocks: ProductStockModel[];

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

}
export interface ProductStockModel {
  Id: number;
  ProductId: number;
  SizeId: number | undefined;
  Size: string;
  UnitPrice: number | null;
  Quantity: number | null;
}
