import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class GeneralEntryService{


  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryList_Api}`;
    return this._baseService.post(url, model);
  }

  GetGeneralEntryCategory(id: number): Observable<ApiResponse<GeneralEntryCategoryViewModel>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateGeneralEntryCategory(model: GeneralEntryCategoryPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeGeneralEntryCategoryActiveStatus(id: number, status?: string): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  ChangeGeneralEntryCategoryFlagStatus(id: number, columnName: string): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryChangeFlagStatus_Api}${id}/${columnName}`;
    return this._baseService.get(url);
  }
  DeleteGeneralEntryCategory(id: number): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}

export interface GeneralEntryCategoryPostModel {
  Id: number;
  Name: string;
  EnumValue: string;
  ImagePath: string;
  IsShowInMain: boolean;
  IsShowDataInMain: boolean;
  IsSingleEntry: boolean;
  SortedOrder: number | null;
}

export interface GeneralEntryCategoryViewModel {
  Id: number;
  Name: string;
  EnumValue: string;
  ImagePath: string;
  IsShowInMain: boolean;
  IsShowDataInMain: boolean;
  IsSingleEntry: boolean;
  IsSystemEntry:boolean;
  SortedOrder: number | null;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;
  IsActive: boolean | null;
  IsDelete: boolean;
}
