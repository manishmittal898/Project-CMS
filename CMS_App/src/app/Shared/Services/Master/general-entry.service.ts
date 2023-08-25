import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class GeneralEntryService {


  constructor(private readonly _baseService: BaseAPIService) { }

  GetListGeneralEntry(model: IndexModel): Observable<ApiResponse<GeneralEntryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryList_Api}`;
    return this._baseService.post(url, model);
  }

  GetGeneralEntry(id: string, isEdit: boolean = false): Observable<ApiResponse<GeneralEntryViewModel>> {
    let url = `${this._baseService.API_Url.GeneralEntryDetail_Api}${id}/${isEdit}`;
    return this._baseService.get(url);
  }

  AddUpdateGeneralEntry(model: GeneralEntryPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.GeneralEntryAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeGeneralEntryActiveStatus(id: string, status?: string): Observable<ApiResponse<GeneralEntryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }

  DeleteGeneralEntry(id: string): Observable<ApiResponse<GeneralEntryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryDelete_Api}${id}/${status}`;
    return this._baseService.get(url);
  }
  DeleteGeneralEntryItems(id: string): Observable<ApiResponse<GeneralEntryDataViewModel>> {
    let url = `${this._baseService.API_Url.GeneralEntryItems_Delete_Api}${id}`;
    return this._baseService.get(url);
  }



  GetListEntryCategory(model: IndexModel): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryList_Api}`;
    return this._baseService.post(url, model);
  }

  GetGeneralEntryCategory(id: string): Observable<ApiResponse<GeneralEntryCategoryViewModel>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateGeneralEntryCategory(model: GeneralEntryCategoryPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeGeneralEntryCategoryActiveStatus(id: string, status?: string): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  ChangeGeneralEntryCategoryFlagStatus(id: string, columnName: string): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryChangeFlagStatus_Api}${id}/${columnName}`;
    return this._baseService.get(url);
  }
  DeleteGeneralEntryCategory(id: string): Observable<ApiResponse<GeneralEntryCategoryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntryCategoryDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}

export interface GeneralEntryCategoryPostModel {
  Id: string;
  Name: string;
  EnumValue: string;
  ImagePath: string;
  ContentType: string;
  IsShowInMain: boolean;
  IsShowDataInMain: boolean;
  IsSingleEntry: boolean;
  IsShowThumbnail: boolean;
  IsShowUrl: boolean;
  SortedOrder: number;
}

export interface GeneralEntryCategoryViewModel {
  Id: string;
  Name: string;
  EnumValue: string;
  ImagePath: string;
  ContentType: string;
  ContentTypeText: string;
  IsShowInMain: boolean;
  IsShowDataInMain: boolean;
  IsSingleEntry: boolean;
  IsSystemEntry: boolean;
  IsShowThumbnail: boolean;
  IsShowUrl: boolean;
  SortedOrder: number | null;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;
  IsActive: boolean | null;
  IsDelete: boolean;
}


export interface GeneralEntryPostModel {
  Id: string;
  CategoryId: string;
  Title: string;
  Description: string;
  SortedOrder: number | null;
  ImagePath: string;
  Url: string;
  Data: string[] | null;
  Keyword: string;
}

export interface GeneralEntryViewModel {
  Id: string;
  Title: string;
  CategoryId: string;
  Category: string;
  ImagePath: string;
  Url: string;
  Description: string;
  DataId: string;
  SortedOrder: number | null;
  IsActive: boolean | null;
  IsDeleted: boolean;
  Data: GeneralEntryDataViewModel[];
  Keyword: string;
}

export interface GeneralEntryDataViewModel {
  Id: string;
  Value: string;
  GeneralEntryId: string;
}
