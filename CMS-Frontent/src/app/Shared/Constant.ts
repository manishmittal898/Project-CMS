import { environment } from './../../environments/environment';


export class API_Url {
  //#region  << Product  >>
  public static Product_List_Api = `${environment.apiEndPoint}public/Product/GetList`;
  public static Product_Category_Api = `${environment.apiEndPoint}public/Product/GetProductCategory`;

  //#endregion
}
