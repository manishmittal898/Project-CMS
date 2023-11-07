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

  SocialLogin(model: any): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.SocialLogin_Api}`;
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

  UpdateProfile(model: UserPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.UserAccount_Save_Api}`;
    return this._baseService.post(url, model);
  }
  GetUserDetail(): Observable<ApiResponse<UserPostModel>> {
    let url = `${this._baseService.API_Url.UserAccount_Detail_Api}`;
    return this._baseService.get(url);
  }

  ChangePassword(model: ChangePasswordModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.ChangePassword_Api}`;
    return this._baseService.post(url, model);
  }



}

export interface UserPostModel {
  Email: string;
  FirstName: string;
  LastName: string;
  Dob: Date;
  Mobile: string;
  ProfilePhoto: string;
  GenderId: string;
}
export interface ChangePasswordModel {
  Email: string;
  Password: string;
  SessionID: string;
  OTP: string;
}
