import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class SubLookupService {
  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<SubLookupMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.SubLookupMasterList_Api}`;
    return this._baseService.post(url, model);
  }

  GetLookupMaster(id: number): Observable<ApiResponse<SubLookupMasterViewModel>> {
    let url = `${this._baseService.API_Url.SubLookupMasterDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateLookupMaster(model: SubLookupMasterPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.SubLookupMasterAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeLookupMasterActiveStatus(id: number, status?: string): Observable<ApiResponse<SubLookupMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.SubLookupMasterChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteLookupMaster(id: number): Observable<ApiResponse<SubLookupMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.SubLookupMasterDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}
export interface SubLookupMasterPostModel {
  Id: number;
  Name: string;
  ImagePath: string;
  SortedOrder: number | null;
  LookUpId: number;
}

export interface SubLookupMasterViewModel {
  Id: number;
  Name: string;
  ImagePath: string;
  SortedOrder: number | null;
  LookUpId: number;
  LookUpName: string;
  CreatedOn: string;
  CreatedBy: number | null;
  ModifiedOn: string;
  ModifiedBy: number | null;
  IsActive: boolean | null;
  IsDeleted: boolean;
}
