import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class LookupTypeService {

  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<LookupTypeMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupTypeMasterList_Api}`;
    return this._baseService.post(url, model);
  }

  GetLookupTypeMaster(id: number): Observable<ApiResponse<LookupTypeMasterModel>> {
    let url = `${this._baseService.API_Url.LookupTypeMasterDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateLookupTypeMaster(model: LookupTypeMasterModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.LookupTypeMasterAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeLookupTypeMasterActiveStatus(id: number, status?: string): Observable<ApiResponse<LookupTypeMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupTypeMasterChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteLookupTypeMaster(id: number): Observable<ApiResponse<LookupTypeMasterModel[]>> {
    let url = `${this._baseService.API_Url.LookupTypeMasterDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}


export interface LookupTypeMasterModel {
  Id: number;
  Name: string;
  EnumValue: string;
  CreatedBy: number;
  CreatedOn: Date;
  ModifiedBy: number;
  ModifiedOn: Date;
  IsActive: boolean;
  IsImage: boolean;
}
