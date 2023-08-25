import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<UserMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.User_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetUserMaster(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.User_Detail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateUserMaster(model: UserViewPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.User_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeUserMasterActiveStatus(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.User_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteUserMaster(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.User_Delete_Api}${id}`;
    return this._baseService.get(url);
  }
}

export interface UserMasterViewModel {
  UserId: number;
  Email: string;
  FirstName: string;
  LastName: string;
  Dob: string | null;
  Address: string;
  Password: string;
  Mobile: string;
  RoleId: number;
  Role: string;
  ProfilePhoto: string;
  CreatedOn: string | null;
  CreatedBy: number | null;
  ModifiedOn: string | null;
  ModifiedBy: number | null;
  IsActive: boolean | null;
  IsDeleted: boolean;
  CustomerAddresses: UserAddressMasterViewModel[] | null;
}

export interface UserViewPostModel {
  UserId: number;
  Email: string;
  FirstName: string;
  LastName: string;
  Dob: string | null;
  Address: string;
  Password: string;
  Mobile: string;
  RoleId: number;
  ProfilePhoto: string;
  CreatedBy: number | null;
  ModifiedBy: number | null;
}

export interface UserAddressMasterViewModel {
  Id: number;
  UserId: number;
  FullName: string;
  Mobile: string;
  BuildingNumber: string;
  Address: string;
  PinCode: string;
  Landmark: string;
  City: string;
  StateId: string ;
  State: string;
  AddressType: string ;
  AddressTypeName: string;
  IsPrimary: boolean;
  IsActive: boolean | null;
  IsDelete: boolean;
}
