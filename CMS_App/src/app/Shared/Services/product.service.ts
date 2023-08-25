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

  GetProductMaster(id: string): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_Detail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateProductMaster(model: ProductMasterPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Product_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeProductMasterActiveStatus(id: string): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteProductMaster(id: string): Observable<ApiResponse<ProductMasterViewModel>> {
    let url = `${this._baseService.API_Url.Product_Delete_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteProductFile(id: string): Observable<ApiResponse<ProductImageViewModel>> {
    let url = `${this._baseService.API_Url.ProductFile_Delete_Api}${id}`;
    return this._baseService.get(url);
  }
}

export interface ProductMasterViewModel {
  Id: string;
  Name: string;
  ImagePath: string;
  CategoryId: string;
  SubCategoryId: string;
  Desc: string;
  Price: number | null;
  CaptionTagId: string | null;
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
  IsWhishList: boolean;
  ViewSectionId: string,
  ViewSection: string,
  Files: ProductImageViewModel[];
  Stocks: ProductStockModel[];

}

export interface ProductMasterPostModel {
  Id: string;
  Name: string;
  ImagePath: string;
  Desc: string;
  Price: number | undefined;
  CategoryId: string | undefined;
  SubCategoryId: string | undefined;
  CaptionTagId: string | undefined;
  ViewSectionId: string | undefined;
  Summary: string;
  ShippingCharge: number | undefined;
  Keyword: string;
  Files?: string[];
  MetaTitle: string;
  MetaDesc: string;
  Stocks: ProductStockModel[];
}

export interface ProductImageViewModel {
  Id: string;
  FilePath: string;
  ProductId: string | null;
}

export interface ProductStockModel {
  Id: string;
  ProductId: string;
  SizeId: string | undefined;
  Size: string;
  UnitPrice: number | null;
  Quantity: number | null;
}
