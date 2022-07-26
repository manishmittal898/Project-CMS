import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDictionary, ApiResponse } from '../Helper/Common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { API_Url } from '../Constant';

@Injectable({
  providedIn: 'root'
})
export class BaseAPIService {
  API_Url= API_Url;

  constructor(private readonly _httpClient: HttpClient) { }

  get(endPoint: string, params?: IDictionary<string>): Observable<ApiResponse<any>> {
      let httpParams;
      if (params) {
          httpParams = this.buildParams(params);
      }
      return this._httpClient.get<ApiResponse<any>>(endPoint, { params: httpParams }).pipe(map(res => JSON.parse(JSON.stringify(res))));

  }

  post(endPoint: string, requestObject: any): Observable<ApiResponse<any>> {

      return this._httpClient.post(endPoint,  requestObject, { headers: { 'Accept': 'application/*' } }).pipe(map(res => JSON.parse(JSON.stringify(res))));
  }

  public put(endPoint: string, requestObject: any): Observable<ApiResponse<any>> {
      return this._httpClient.put<ApiResponse<any>>(endPoint, requestObject).pipe(map(res => JSON.parse(JSON.stringify(res))));
  }

  public Delete(endPoint: string, params?: IDictionary<string>): Observable<ApiResponse<any>> {
      let httpParams;
      if (params) {
          httpParams = this.buildParams(params);
      }
      return this._httpClient.delete<ApiResponse<any>>(endPoint, { params: httpParams }).pipe(map(res => JSON.parse(JSON.stringify(res))));
  }



  /**
    * buildParams - Converts from Dictionary to HttpParams
    */
  public buildParams(params: IDictionary<string>): HttpParams {
      let httpParams = new HttpParams();
      if (params) {
          const keys: string[] = params.Keys();
          keys.forEach(key => {
              httpParams = httpParams.append(key, params.Item(key));
          });
      }
      return httpParams;
  }
}
