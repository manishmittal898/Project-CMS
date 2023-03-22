import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import * as CryptoJS from 'crypto-js';
import { BaseAPIService } from "./base-api.service";
import { Observable } from "rxjs";
import { ApiResponse } from "./common-model";
@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor(private readonly _baseService: BaseAPIService) { }

  setStorage(key: string, value: string) {
    let encValue = this.encrypt(value);
    let encKey = this.encrypt(key)
    localStorage.setItem(key, encValue);
    return true;
  }

  getStorage(key: string) {
    let encKey = this.encrypt(key);
    let decValue = localStorage.getItem(key)
    return decValue ? this.decrypt(decValue) : undefined;
  }

  removeStorage(key: string) {
    let encKey = this.encrypt(key);
    localStorage.removeItem(key);
  }

  // encrypt(txt: string) {
  //   const IV = [10, 20, 30, 40, 50, 60, 70, 80];

  //   return CryptoJS.AES.encrypt(txt, environment.AESKey.trim()).toString();
  // }

  // decrypt(txt: string) {
  //   const IV = [10, 20, 30, 40, 50, 60, 70, 80];
  //   return CryptoJS.AES.decrypt(txt, environment.AESKey.trim()).toString(CryptoJS.enc.Utf8);
  // }


  encrypt(txt: string) {
    const key = CryptoJS.enc.Hex.parse(environment.AESKey.trim()); // 256-bit key
    return CryptoJS.AES.encrypt(txt, key, { mode: CryptoJS.mode.ECB }).toString();
  }


  decrypt(txt: string) {
    const key = CryptoJS.enc.Hex.parse(environment.AESKey.trim()); // 256-bit key
    return CryptoJS.AES.decrypt(txt, key, { mode: CryptoJS.mode.ECB, pad: CryptoJS.pad.Pkcs7 }).toString(CryptoJS.enc.Utf8);

  }

  GenerateEncrptPassword(value: string): Promise<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.GenerateEncrptPassword_Api}?value=${value}`;
    return this._baseService.get(url).toPromise();
  }


}
