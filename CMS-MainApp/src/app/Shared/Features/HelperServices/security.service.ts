import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import * as CryptoJS from 'crypto-js';
@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor() { }

  setStorage(key : string, value : string) {
    let encValue = this.encrypt(value);
    let encKey = this.encrypt(key)
    localStorage.setItem(key, encValue);
    return true;
  }

  getStorage(key:string) {
    let encKey = this.encrypt(key);
    let decValue = localStorage.getItem(key)
    return decValue ? this.decrypt(decValue) : undefined;
  }

  removeStorage(key:string) {
    let encKey = this.encrypt(key);
    localStorage.removeItem(key);
  }

  encrypt(txt:string) {
    return CryptoJS.AES.encrypt(txt, environment.AESKey.trim()).toString();
  }

  decrypt(txt :string) {
    return CryptoJS.AES.decrypt(txt, environment.AESKey.trim()).toString(CryptoJS.enc.Utf8);
  }

}
