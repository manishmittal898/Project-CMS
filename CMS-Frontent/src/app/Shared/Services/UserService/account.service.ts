import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse, Dictionary } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly _baseService: BaseAPIService,) { }

  Login(model: any): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.Login_Api}`;
    return this._baseService.post(url, model);
  }
  Register(model: any): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.Register_Api}`;
    return this._baseService.post(url, model);
  }

  // CheckUserExist(loginId: string, isMobile: boolean, userId: number): Promise<ApiResponse<any>> {
  //   let url = `${this._baseService.API_Url.CheckUserExist_Api}`;
  //   let parms= new Dictionary<any>();
  //   parms.Add("loginId", loginId)
  //   parms.Add("isMobile", isMobile)
  //   parms.Add("userId", userId)
  //   return this._baseService.get(url, parms).toPromise();
  // }

  CheckUserExist(loginId: string, isMobile: boolean, userId: number): Promise<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.CheckUserExist_Api}`;
    let parms = new Dictionary<any>();
    parms.Add("loginId", loginId)
    parms.Add("isMobile", isMobile)
    parms.Add("userId", userId)
    return new Promise((resolve, reject) => {
      return this._baseService.get(url, parms).subscribe(
        data => {
          resolve(data);
        },
        error => {
          reject(error);
        }
      );
    });
  }

  UpdateProfile(model: UserViewPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAccount_Save_Api}`;
    return this._baseService.post(url, model);
  }
  GetUserDetail(userId: any): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAccount_Detail_Api}`;
    return this._baseService.get(url + userId);
  }

}

export interface UserViewPostModel {
  UserId: number;
  Email: string;
  FirstName: string;
  LastName: string;
  Dob: string | null;
  Mobile: string;
  ProfilePhoto: string;

}
