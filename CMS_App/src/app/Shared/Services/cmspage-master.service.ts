import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../Helper/common-model';
import { LookupMasterModel } from './Master/lookup.service';

@Injectable({
  providedIn: 'root'
})
export class CMSPageMasterService {


  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<CMSPageListViewModel[]>> {
    let url = `${this._baseService.API_Url.CMSPageList_Api}`;
    return this._baseService.post(url, model);
  }

  GetDetails(id: number): Observable<ApiResponse<CMSPageViewModel[]>> {
    let url = `${this._baseService.API_Url.CMSPageDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateCMSPage(model: CMSPagePostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.CMSPageAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeCMSPageActiveStatus(id: number, status?: string): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupMasterChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteCMSPage(id: number): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupMasterDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }

  ChangeCMSContentActiveStatus(id: number): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.CMSContentChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteCMSContent(id: number): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.CMSContentDelete_Api}${id}`;
    return this._baseService.Delete(url);
  }
}
export interface CMSPageViewModel {
  Id: number;
  PageId: number;
  Heading: string;
  Content: string;
  SortedOrder: number | null;
  IsActive: boolean | null;
  IsDeleted: boolean;
  Page: string;
}

export interface CMSPageListViewModel {
  PageId: number;
  Name: string;
  SortedOrder: number | null;
  IsActive: boolean;
  IsDelete: boolean;
}



export interface CMSPagePostModel {
  PageId: number;
  Id: number;
  Heading: string;
  Content: string;
  SortedOrder: number | null;
}
