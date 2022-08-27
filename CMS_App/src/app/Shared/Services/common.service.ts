import { AlertService } from './alert.service';
import { ApiResponse, FilterDropDownPostModel } from './../Helper/common-model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../Helper/base-api.service';
import { mode } from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CommonService extends AlertService {

  constructor(private readonly _baseService: BaseAPIService) {
    super();
  }

  GetDropDown(key: string[], isTransactionData = false): Observable<ApiResponse<any>> {

    return this._baseService.post(`${this._baseService.API_Url.DropDown_Api}/${isTransactionData}`, key);
  }

  GetFilterDropDown(model: FilterDropDownPostModel): Observable<ApiResponse<any>> {

    return this._baseService.post(this._baseService.API_Url.FilterDropDown_Api, model);
  }

  GetMultipleFilterDropDown(model: FilterDropDownPostModel[]): Observable<ApiResponse<any>> {

    return this._baseService.post(this._baseService.API_Url.MultipleFilterDropDown_Api, model);
  }

  NumberOnly(event: any, isCommaOrDash: boolean = false): boolean {

    const charCode = event.which ? event.which : event.keyCode;
    if (isCommaOrDash) {
      if (charCode == 44 || charCode == 45) {
        return true;
      }
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  MaskString(strValue: string, lastShowDigit: number) {
    return strValue.replace(/\d(?=\d{`${lastShowDigit}`})/g, "X");
  }

  checkDecimalNumberOnly(event: any): boolean {

    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode == 46) {
      //Check if the text already contains the . character
      if (event.target.value.indexOf('.') === -1) {
        return true;
      } else {
        return false;
      }
    }
    else {
      if (event.target.value.split('.').length > 1 && event.target.value.split('.')[1].length > 1) {
        return false;
      }
      else if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    }
    return true;
  }

  AlphaNumericOnly(e: any) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[A-Za-z0-9]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (!isValid) {
      //alert("Only Alphabets and Numbers are allowed.");
    }
    return isValid;
  }

  AlphabetOnly(e: any) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[a-zA-Z& ]*$/;;
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (!isValid) {
      //alert("Only Alphabets and Numbers are allowed.");
    }
    return isValid;
  }

}
