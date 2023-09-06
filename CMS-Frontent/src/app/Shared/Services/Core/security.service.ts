import { Injectable } from "@angular/core";
import * as CryptoJS from 'crypto-js';
import { BaseAPIService } from "./base-api.service";
import { ApiResponse } from '../../Helper/Common';
import { environment } from "src/environments/environment";
import { CookieService } from "ngx-cookie-service";

@Injectable({
  providedIn: 'root',

})
export class SecurityService {

  constructor(private readonly _baseService: BaseAPIService, private readonly _cookie: CookieService) { }

  setStorage(key: string, value: string, storeInSession = false) {
    const encKey = this.getKey(key, storeInSession) ?? this.encrypt(key);
    const encValue = this.encrypt(value);
    storeInSession ? sessionStorage.setItem(encKey, encValue) : localStorage.setItem(encKey, encValue);
    return true;
  }

  deleteStorage(key, storeInSession = false) {
    const encKey = this.getKey(key, storeInSession) ?? this.encrypt(key);
    return storeInSession ? sessionStorage.delete(encKey) : localStorage.delete(encKey);
  }
  getStorage(key: string, storeInSession = false) {

    const encKey = this.getKey(key, storeInSession) ?? key
    let decValue = storeInSession ? sessionStorage.getItem(encKey) : localStorage.getItem(encKey);

    return decValue ? this.decrypt(decValue) : undefined;
  }
  checkLocalStorage(key, storeInSession = false) {
    return this.getKey(key, storeInSession);
  }

  removeStorage(key: string, storeInSession = false) {
    const encKey = this.getKey(key, storeInSession) ?? this.encrypt(key);

    let decValue = storeInSession ? sessionStorage.delete(encKey) : localStorage.delete(encKey);

  }
  setCookie(key: string, value: string) {
    const encKey = this.getCookieKey(key) ?? this.encrypt(key);
    const encValue = this.encrypt(value);
    this._cookie.set(encKey, encValue, 3, '');
    return true;
  }
  checkCookie(key) {
    const encKey = this.getCookieKey(key) ?? this.encrypt(key);
    return this._cookie.check(encKey);
  }
  deleteCookie(key) {
    const encKey = this.getCookieKey(key) ?? this.encrypt(key);
    return this._cookie.delete(encKey);
  }
  getCookie(key: string) {

    const encKey = this.getCookieKey(key) ?? key
    //  const decValue = localStorage.getItem(encKey);
    const decValue = this._cookie.get(encKey);

    return decValue ? this.decrypt(decValue) : undefined;
  }

  removeCookie(key: string) {
    const encKey = this.getCookieKey(key) ?? this.encrypt(key);
    this._cookie.delete(encKey);

  }


  encrypt(txt: string) {
    try {

      return CryptoJS.AES?.encrypt(txt, environment.AESKey?.trim()).toString() ?? null;
    } catch (error) {
      return null
    }
  }

  decrypt(txt: string) {
    try {

      return CryptoJS.AES?.decrypt(txt, environment.AESKey?.trim()).toString(CryptoJS.enc.Utf8) ?? null;
    } catch (error) {
      return null
    }
  }
  private getCookieKey(key) {
    try {
      var keys = Object.keys(this._cookie.getAll());
      for (let index = 0; index < keys.length; index++) {

        if (this.decrypt(keys[index]) === key) {
          return keys[index] ?? null;
        }
      }
    } catch (error) {
      return null;
    }


  }

  private getKey(key, storeInSession = false) {
    try {
      if (storeInSession) {
        for (var itm in sessionStorage) {
          if (this.decrypt(itm) === key) {
            return itm ?? null;
          }
        }
      } else {
        for (var itm in localStorage) {
          if (this.decrypt(itm) === key) {
            return itm ?? null;
          }
        }
      }


    } catch (error) {
      return null;
    }


  }

  GetEncrptedText(value: string): Promise<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.GetEncrptedText_Api}?value=${value}`;
    return this._baseService.get(url).toPromise();
  }


}
