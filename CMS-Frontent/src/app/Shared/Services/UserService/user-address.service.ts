import { BaseAPIService } from './../Core/base-api.service';
import { Injectable } from '@angular/core';
import { ApiResponse, IndexModel } from '../../Helper/Common';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UserAddressService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<UserAddressViewModel[]>> {
    let url = `${this._baseService.API_Url.UserAddress_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetDetail(Id: Number): Observable<ApiResponse<UserAddressViewModel>> {
    let url = `${this._baseService.API_Url.UserAddress_Detail_Api}`;
    return this._baseService.get(`${url + Id}`);
  }
  Save(model: UserAddressPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_Save_Api}`;
    return this._baseService.post(url, model);
  }

  SetPrimary(Id: Number): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_SetDefaultAddress_Api}`;
    return this._baseService.get(`${url + Id}`);
  }

  Delete(Id: Number): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_Delete_Api}`;
    return this._baseService.get(`${url + Id}`);
  }

}
export interface UserAddressPostModel {
  Id: number;
  UserId: number;
  FullName: string;
  Mobile: string;
  BuildingNumber: string;
  Address: string;
  PinCode: string;
  Landmark: string;
  City: string;
  StateId: number | null;
  AddressType: number | null;
  IsPrimary: boolean;
}

export interface UserAddressViewModel {
  Id: number;
  UserId: number;
  FullName: string;
  Mobile: string;
  BuildingNumber: string;
  Address: string;
  PinCode: string;
  Landmark: string;
  City: string;
  StateId: number | null;
  AddressType: number | null;
  IsPrimary: boolean;
  AddressTypeName: string;
  State: string;
}
