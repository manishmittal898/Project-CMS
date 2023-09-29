
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { BehaviorSubject, Subject } from 'rxjs';
import { BaseAPIService } from "../Core/base-api.service";
import { SecurityService } from '../Core/security.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public IsAuthentication = new BehaviorSubject<boolean>(null);

  constructor(private readonly _baseService: BaseAPIService, private _router: Router, private readonly _securityService: SecurityService) {
     this.IsAuthenticate();
  }

  IsAccessibleUrl(requestedUrl: string): boolean {
    return true;
  }



  SaveUserToken(token: string) {
    this._securityService.setStorage('authToken', token)
    this._securityService.setStorage('sessionTime', String(new Date().setHours(24)));
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


  IsAuthenticate() {
    setTimeout(() => {
      let token = this._securityService.getStorage('authToken');
      let sessionTime = this._securityService.getStorage('sessionTime');
      let currentSessionTime = Number(new Date().getTime());
      if (token != null && Number(sessionTime) > currentSessionTime) {
        this.IsAuthentication.next(true);
      } else {
        this.IsAuthentication.next(false);
      }
    }, 10);
  }



  LogOut() {
    if (this.GetUserDetail()?.UserId) {
      let url = `${this._baseService.API_Url.Logout_Api}?id=${this.GetUserDetail()?.UserId}`;
      this._baseService.get(url).subscribe(x => {
        this.removeLocalData();
      }, err => {
        this.removeLocalData();
      });
    }


  }
  private removeLocalData() {
    this.IsAuthentication.next(false);
    this._securityService.removeStorage('authToken');
    this._securityService.removeStorage('sessionTime');
    this._securityService.removeStorage('userDetail');
   // this._securityService.removeStorage('userDetail');

    this._securityService.removeStorage('cart-product');

    setTimeout(() => {
      if (this._router.url.includes('/user')) {
        this._router.navigate([this._baseService.Routing_Url.storeUrl]);
      }
      else if (this._router.url !== this._baseService.Routing_Url.LoginUrl && !this._router.url.includes(this._baseService.Routing_Url.storeUrl)) {
        this._router.navigate([this._baseService.Routing_Url.LoginUrl]);
      }
    }, 10);
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
