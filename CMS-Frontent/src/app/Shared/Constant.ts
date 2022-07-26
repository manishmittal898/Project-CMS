import { environment } from './../../environments/environment';


export class API_Url {
  //#region  << Product  >>
  public static Product_List_Api = `${environment.apiEndPoint}public/Product/GetList`;
  public static Product_Category_Api = `${environment.apiEndPoint}public/Product/GetProductCategory`;
  public static Product_Detail_Api = `${environment.apiEndPoint}public/Product/Get/`;


  //#endregion
}
