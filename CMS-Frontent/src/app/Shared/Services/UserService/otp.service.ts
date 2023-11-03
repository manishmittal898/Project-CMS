import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse, Dictionary } from '../../Helper/Common';
import { BaseAPIService } from '../Core/base-api.service';

@Injectable({
  providedIn: 'root'
})

export class OTPService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetOTP(email: string): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.GetOTP_Api}`;
    const param = new Dictionary<any>();
    param.Add("emailId", email)
    return this._baseService.get(url, param);
  }
  VerifyOTP(model: OTPModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.VerifyOTP_Api}`;
    return this._baseService.post(url, model);
  }
}
export interface OTPModel {
  SessionId: string;
  OTP: string;
}
