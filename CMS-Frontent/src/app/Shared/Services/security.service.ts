import { Injectable } from "@angular/core";
import * as CryptoJS from 'crypto-js';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor() { }

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
    return CryptoJS.DES.encrypt(txt, environment.AESKey?.trim()).toString();
  }

  decrypt(txt: string) {
    return CryptoJS.DES.decrypt(txt, environment.AESKey?.trim()).toString(CryptoJS.enc.Utf8);
  }
  private getKey(key) {

    for (let i = 0; i < localStorage.length; i++) {
      if (this.decrypt(localStorage.key(i)) === key) {
        return localStorage.key(i);
      }

    }
    return null;
  }

}
