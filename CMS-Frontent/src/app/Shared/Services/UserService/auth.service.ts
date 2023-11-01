
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Subject } from 'rxjs';
import { BaseAPIService } from "../Core/base-api.service";
import { SecurityService } from '../Core/security.service';

declare var handleGoogleSignOut: any;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public IsAuthentication = new BehaviorSubject<boolean>(null);

  constructor(private readonly _baseService: BaseAPIService, private _router: Router, private readonly _securityService: SecurityService) {
    // this.IsAuthenticate();
  }

  IsAccessibleUrl(requestedUrl: string): boolean {
    return true;
  }

  googleSignOut() {

  }

  SaveUserToken(token: string, isSocial: boolean = false) {
    this._securityService.setStorage('authToken', token)
    this._securityService.setStorage('sessionTime', String(new Date().setHours(24)));
    this._securityService.setStorage('socialLogin', String(isSocial));
    this.IsAuthentication.next(true);

  }

  SaveUserDetail(model: LoginUserDetailModel) {
    let data = JSON.stringify(model);
    this._securityService.setStorage('userDetail', data);

  }

  GetUserDetail(): LoginUserDetailModel | null {
    let data = this._securityService.getStorage('userDetail');
    if (data) {
      return JSON.parse(data!) as LoginUserDetailModel;
    }
    else {
      return null;
    }
  }


  async IsAuthenticate() {
    // setTimeout(() => {
    let token = this._securityService.getStorage('authToken');
    let sessionTime = this._securityService.getStorage('sessionTime');
    let currentSessionTime = Number(new Date().getTime());
    if (token != null && Number(sessionTime) > currentSessionTime) {
      this.IsAuthentication.next(true);
    } else {
      this.IsAuthentication.next(false);
    }
    return
    //   }, 0);
  }



  LogOut() {
    const userId = this.GetUserDetail()?.UserId;

    if (this._securityService.getStorage("socialLogin") == "true") {

      const d = new Date(-1);
      d.setTime(d.getTime());
      let expires = "expires=" + d.toUTCString();
      document.cookie = 'g_state' + "=" + '' + ";" + expires + "";
      handleGoogleSignOut();
    }
    this.removeLocalData();
    if (userId) {
      let url = `${this._baseService.API_Url.Logout_Api}?id=${userId}`;
      this._baseService.get(url).subscribe(x => {
      }, err => {

      });
    }


  }
  private removeLocalData() {
    // this.IsAuthentication.next(false);
    this._securityService.removeStorage('authToken');
    this._securityService.removeStorage('sessionTime');
    this._securityService.removeStorage('userDetail');
    this._securityService.removeStorage('socialLogin');


    // this._securityService.removeStorage('userDetail');

    this._securityService.removeStorage('cart-product');


  }
}
export interface LoginUserDetailModel {
  UserId: number;
  FullName: string;
  RoleId: number;
  RoleLevel: number;
  Token: string;
  UserName: string;
  RoleName: string;
  ProfilePhoto: string;
}
