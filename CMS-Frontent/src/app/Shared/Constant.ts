import { environment } from './../../environments/environment';



export class DropDown_key {

  static ddlState = "ddlState";
  static ddlAddressType = "ddlAddressType";
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

  //#region <<Login>>
  public static Login_Api = `${environment.apiEndPoint}account/Login`;
  public static GetEncrptedText_Api = `${environment.apiEndPoint}account/GetEncrptedText`;
  public static Register_Api = `${environment.apiEndPoint}account/Register`;
  public static CheckUserExist_Api = `${environment.apiEndPoint}account/CheckUserExist`;
  public static Logout_Api = `${environment.apiEndPoint}account/Logout`;
  //#endregion

  //#region  << User Address  >>
  public static UserAddress_List_Api = `${environment.apiEndPoint}customer/Address/Get`;
  public static UserAddress_Detail_Api = `${environment.apiEndPoint}customer/Address/Get/`;
  public static UserAddress_Save_Api = `${environment.apiEndPoint}customer/Address/Save`;
  public static UserAddress_SetDefaultAddress_Api = `${environment.apiEndPoint}customer/Address/SetPrimary/`;
  public static UserAddress_Delete_Api = `${environment.apiEndPoint}customer/Address/Delete/`;


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

export class Message {
  //#region <<Alert Message >>
  static SaveSuccess = 'Record successfully saved...!';
  static SaveFail = 'Record failed to save...!';
  static UpdateSuccess = 'Record successfully updated...!';
  static UpdateFail = 'Record failed to update...!';
  static DeleteSuccess = 'Record successfully deleted...!';
  static DeleteFail = 'Record failed to Delete...!';

  static ConfirmUpdate = 'Are you Sure update this record?';
  static DeleteConfirmation = 'Are you want to delete record ?';
  static VerifyInput = 'Please Verify input data?';


  //#endregion
}
