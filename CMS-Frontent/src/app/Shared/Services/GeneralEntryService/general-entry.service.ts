import { BaseAPIService } from './../Core/base-api.service';
import { Observable } from 'rxjs';
import { ApiResponse, IndexModel } from './../../Helper/Common';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GeneralEntryService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: GeneralEntryFilterModel): Observable<ApiResponse<GeneralEntryViewModel[]>> {
    let url = `${this._baseService.API_Url.GeneralEntry_List_Api}`;
    return this._baseService.post(url, model);
  }

}

export class GeneralEntryEnumValue {
  static Banner_Image = 'Banner_Image'
}

export class GeneralEntryFilterModel extends IndexModel {
  constructor() {
    super()
  }

  Title: string;
  CategoryId: string | null;
  EnumValue: string;
}

export interface GeneralEntryViewModel {
  Id: string;
  Title: string;
  CategoryId: string;
  Category: string;
  ImagePath: string;
  Url: string;
  Description: string;
  DataId: string;
  SortedOrder: number | null;
  Keyword: string;
  IsActive: boolean | null;
  IsDeleted: boolean;
  Data: GeneralEntryDataViewModel[] | null;
}

export interface GeneralEntryDataViewModel {
  Id: string;
  Value: string;
  GeneralEntryId: string | null;
}
