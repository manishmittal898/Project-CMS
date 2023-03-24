import { environment } from './../../environments/environment';



export class DropDown_key {

  static ddlState = "ddlState";
  static ddlDistrict = "ddlDistrict";
  static ddlLookupTypeMasters = "ddlLookupTypeMaster";
  static ddlCaptionTag = "ddlCaptionTag";
  static ddlCategory = "ddlCategory";
  static ddlProductSize = "ddlProductSize"
  static ddlLookup = "ddllookup"
  static ddlLookupGroup = "ddlLookupGroup";
  static ddlSublookup = "ddlSublookup";
  static ddlSubLookupGroup = "ddlSubLookupGroup";
  static ddlCMSPage = "ddlCMSPage";
  static ddlProductPrice = "ddlProductPrice";

}

export class API_Url {

  //#region <<Common >>
  public static DropDown_Api = `${environment.apiEndPoint}Dropdown/GetDropDown`;
  public static FilterDropDown_Api = `${environment.apiEndPoint}Dropdown/GetFilterDropDown`;
  public static MultipleFilterDropDown_Api = `${environment.apiEndPoint}Dropdown/GetMultipleFilterDropDown`;

  //#endregion


  //#region  << Product  >>
  public static Product_List_Api = `${environment.apiEndPoint}public/Product/GetList`;
  public static Product_Category_Api = `${environment.apiEndPoint}public/Product/GetProductCategory`;
  public static Product_Detail_Api = `${environment.apiEndPoint}public/Product/Get/`;


  //#endregion

  //#region  << CMS Page  >>
  public static CMSPageDetail_Api = `${environment.apiEndPoint}public/CMSPage/Get/`;


  //#endregion



}

export class Routing_Url {

  //#region <<Module URL>>

  public static MasterModule = 'master';

  //#endregion

  //#region <<Login URL>>
  public static LoginUrl = 'login';

  //#endregion

}
