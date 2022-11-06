import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<ProductMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.Product_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetProductMaster(id: number): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_Detail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateProductMaster(model: ProductMasterPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Product_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeProductMasterActiveStatus(id: number): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteProductMaster(id: number): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_Delete_Api}${id}`;
    return this._baseService.Delete(url);
  }
  DeleteProductFile(id: number): Observable<ApiResponse<ProductImageViewModel>> {
    let url = `${this._baseService.API_Url.ProductFile_Delete_Api}${id}`;
    return this._baseService.Delete(url);
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
  ShippingCharge: number | undefined;
  Keyword: string;
  MetaTitle: string;
  MetaDesc: string;
  Files: ProductImageViewModel[];
  Stocks: ProductStockModel[];

}

export interface ProductMasterPostModel {
  Id: number;
  Name: string;
  ImagePath: string;
  Desc: string;
  Price: number | undefined;
  CategoryId: number | undefined;
  SubCategoryId: number | undefined;
  CaptionTagId: number | undefined;
  Summary: string;
  ShippingCharge: number | undefined;
  Keyword: string;
  Files?: string[];
  MetaTitle: string;
  MetaDesc: string;
  Stocks: ProductStockModel[];
}

export interface ProductImageViewModel {
  Id: number;
  FilePath: string;
  ProductId: number | null;
}

export interface ProductStockModel {
  Id: number;
  ProductId: number;
  SizeId: number | undefined;
  Size: string;
  UnitPrice: number | null;
  Quantity: number | null;
}
