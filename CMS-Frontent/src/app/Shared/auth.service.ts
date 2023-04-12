
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Subject } from 'rxjs';
import { BaseAPIService } from './Services/base-api.service';
import { SecurityService } from './Services/security.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public IsAuthentication = new Subject<boolean>();

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

  SaveUserDetail(model: any) {
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

        // this.LogOut();
        this.IsAuthentication.next(false);
      }
    }, 5);
  }



  LogOut() {
    let url = `${this._baseService.API_Url.Logout_Api}?id=${this.GetUserDetail()?.UserId}`;
    this._baseService.get(url).subscribe(x => {

      this.removeLocalData();
    }, err => {
      this.removeLocalData();
    });
  }
  private removeLocalData() {
    this.IsAuthentication.next(false);
    this._securityService.removeStorage('authToken');
    this._securityService.removeStorage('sessionTime');
    this._securityService.removeStorage('userDetail');

    setTimeout(() => {
      if (this._router.url !== this._baseService.Routing_Url.LoginUrl) {
        this._router.navigate([this._baseService.Routing_Url.LoginUrl]);
      }
    }, 10);
  }
}
export interface LoginUserDetailModel {
  UserId: number;
  RoleId: number;
  RoleLeve: number;
  Token: string;
  UserName: string;
  RoleName: string;
  ProfilePhoto: string;
}
