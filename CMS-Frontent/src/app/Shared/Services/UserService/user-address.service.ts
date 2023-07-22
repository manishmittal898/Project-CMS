import { BaseAPIService } from './../Core/base-api.service';
import { Injectable } from '@angular/core';
import { ApiResponse, IndexModel } from '../../Helper/Common';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class UserAddressService {

  constructor(private readonly _baseService: BaseAPIService, private readonly _httpClient: HttpClient) { }

  GetList(model: IndexModel): Observable<ApiResponse<UserAddressViewModel[]>> {
    let url = `${this._baseService.API_Url.UserAddress_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetDetail(Id: string): Observable<ApiResponse<UserAddressViewModel>> {
    let url = `${this._baseService.API_Url.UserAddress_Detail_Api}`;
    return this._baseService.get(`${url + Id}`);
  }
  Save(model: UserAddressPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_Save_Api}`;
    return this._baseService.post(url, model);
  }

  SetPrimary(Id: string): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_SetDefaultAddress_Api}`;
    return this._baseService.get(`${url + Id}`);
  }

  Delete(Id: string): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAddress_Delete_Api}`;
    return this._baseService.get(`${url}?id=${Id}`);
  }

  getAddressDetailByPinCode(pinCode: number) {
    let url = `https://api.postalpincode.in/pincode/${pinCode}`;
    return this._httpClient.get<any>(`${url}`);
  }

}
export interface UserAddressPostModel {
  Id: string;
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
  Id: string;
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
