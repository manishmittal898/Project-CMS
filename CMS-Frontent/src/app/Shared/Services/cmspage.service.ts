import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../Helper/Common';
import { BaseAPIService } from './Core/base-api.service';

@Injectable({
  providedIn: 'root'
})
export class CMSPageService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetDetails(id: number): Observable<ApiResponse<CMSPageViewModel[]>> {
    let url = `${this._baseService.API_Url.CMSPageDetail_Api}${id}`;
    return this._baseService.get(url);
  }



}

export interface CMSPageViewModel {
  Id: number;
  PageId: number;
  Heading: string;
  Content: string;
  SortedOrder: number | null;
  IsActive: boolean | null;
  IsDeleted: boolean;
  Page: string;
}
