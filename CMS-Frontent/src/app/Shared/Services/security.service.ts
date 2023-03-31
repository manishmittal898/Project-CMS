import { Injectable } from "@angular/core";
import * as CryptoJS from 'crypto-js';
import { environment } from '../../../environments/environment';
import { ApiResponse } from '../Helper/Common';
import { BaseAPIService } from "./base-api.service";

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor(private readonly _baseService: BaseAPIService) { }

  setStorage(key: string, value: string) {
    const encKey = this.getKey(key) ?? this.encrypt(key);
    const encValue = this.encrypt(value);

    localStorage.setItem(encKey, encValue);
    return true;
  }

  getStorage(key: string) {
    const encKey = this.getKey(key) ?? this.encrypt(key)
    const decValue = localStorage.getItem(encKey)
    return decValue ? this.decrypt(decValue) : undefined;
  }

  removeStorage(key: string) {
    const encKey = this.getKey(key) ?? this.encrypt(key);
    localStorage.removeItem(encKey);
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
  private getKey(key) {
    try {
      for (let i = 0; i < localStorage?.length; i++) {

        if (this.decrypt(localStorage?.key(i)) === key) {
          return localStorage.key(i) ?? null;
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
