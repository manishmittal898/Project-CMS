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
  CategoryId!: string[];
  SubCategoryId!: string[];
  Price!: any[];
  IsAvailableStock!: boolean;
  SizeId!: string[];
  Keyword!: string;
  ViewSectionId!: string[];
  Ids!: string[]
}


export interface ProductMasterViewModel {
  Id: string;
  Name: string;
  ImagePath: string;
  CategoryId: string;
  SubCategoryId: string;
  Desc: string;
  Price: number | null;
  CaptionTagId: string ;
  Summary: string;
  CaptionTag: string;
  Category: string;
  SubCategory: string;
  ShippingCharge: string;
  IsWhishList: boolean;
  ViewSectionId: string;
  Files: ProductImageViewModel[];
  Stocks: ProductStockModel[];

}

export interface ProductImageViewModel {
  Id: string;
  FilePath: string;
  ProductId: string;
  ThumbnailPath: string;
}


export interface ProductCategoryViewModel {
  Id: string;
  Name: string;
  ImagePath: string;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;

}
export interface ProductStockModel {
  Id: string;
  ProductId: string;
  SizeId: string | undefined;
  Size: string;
  UnitPrice: number | null;
  Quantity: number | null;
}
