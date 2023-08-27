import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupMasterList_Api}`;
    return this._baseService.post(url, model);
  }

  GetLookupMaster(id: string): Observable<ApiResponse<LookupMasterModel>> {
    let url = `${this._baseService.API_Url.LookupMasterDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateLookupMaster(model: LookupMasterPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.LookupMasterAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeLookupMasterActiveStatus(id: string, status?: string): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupMasterChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteLookupMaster(id: string): Observable<ApiResponse<LookupMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupMasterDelete_Api}${id}`;
    return this._baseService.get(url);
  }
}

export interface LookupMasterPostModel {
  Id: string;
  Name: string;
  Value: string;
  ImagePath: string;
  SortedOrder: number;
  LookUpType: string;

}

export interface LookupMasterModel {

  Id: string;
  Name: string;
  Value:string;
  ImagePath: string;
  SortedOrder: number | null;
  LookUpType: string | null;
  LookUpTypeName: string;
  IsSubLookup: boolean;
  CreatedBy: number;
  CreatedOn: string;
  ModifiedBy: number;
  ModifiedOn: string;
  IsActive: boolean | null;

}










