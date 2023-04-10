import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../Helper/common-model';
import { UserMasterViewModel, UserViewPostModel } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private readonly _baseService: BaseAPIService) { }
  GetCustomerList(model: IndexModel): Observable<ApiResponse<UserMasterViewModel[]>> {
    let url = `${this._baseService.API_Url.Customer_List_Api}`;
    return this._baseService.post(url, model);
  }

  GetCustomerDetail(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.Customer_Detail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateCustomer(model: UserViewPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Customer_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeCustomerActiveStatus(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.Customer_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteCustomer(id: number): Observable<ApiResponse<UserMasterViewModel>> {
    let url = `${this._baseService.API_Url.Customer_Delete_Api}${id}`;
    return this._baseService.get(url);
  }
}
