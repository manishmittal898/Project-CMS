import { environment } from 'src/environments/environment';

export class API_Url {

  //#region <<Login>>
  public static Login_Api = `${environment.apiEndPoint}account/Login`;
  //#endregion

  //#region <<Common >>
  public static DropDown_Api = `${environment.apiEndPoint}Dropdown/GetDropDown`;
  public static FilterDropDown_Api = `${environment.apiEndPoint}Dropdown/GetFilterDropDown`;
  public static MultipleFilterDropDown_Api = `${environment.apiEndPoint}Dropdown/GetMultipleFilterDropDown`;

  //#endregion

  //#region <<User Role>>
  public static UserRole_Dropdown_Api = `${environment.apiEndPoint}UserRole/Roles`;
  public static UserRoleList_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static UserRoleDetail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static UserRoleAddUpdate_Api = `${environment.apiEndPoint}UserRole/post`;
  public static UserRoleCheckRoleExist_Api = `${environment.apiEndPoint}UserRole/CheckRoleExist/`;
  public static UserRoleChangeActiveStatus_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static UserRoleDelete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<LookupTypeMaster>>
  public static LookupTypeMasterList_Api = `${environment.apiEndPoint}LookupTypeMaster/get`;
  public static LookupTypeMasterDetail_Api = `${environment.apiEndPoint}LookupTypeMaster/get/`;
  public static LookupTypeMasterAddUpdate_Api = `${environment.apiEndPoint}LookupTypeMaster/post`;
  public static LookupTypeMasterChangeActiveStatus_Api = `${environment.apiEndPoint}LookupTypeMaster/ChangeActiveStatus/`;
  public static LookupTypeMasterDelete_Api = `${environment.apiEndPoint}LookupTypeMaster/delete/`;
  //#endregion

  //#region <<LookupMaster>>
  public static LookupMasterList_Api = `${environment.apiEndPoint}LookupMaster/get`;
  public static LookupMasterDetail_Api = `${environment.apiEndPoint}LookupMaster/get/`;
  public static LookupMasterAddUpdate_Api = `${environment.apiEndPoint}LookupMaster/Save`;
  public static LookupMasterChangeActiveStatus_Api = `${environment.apiEndPoint}LookupMaster/ChangeActiveStatus/`;
  public static LookupMasterDelete_Api = `${environment.apiEndPoint}LookupMaster/delete/`;
  //#endregion

    //#region <<SubLookupMaster>>
    public static SubLookupMasterList_Api = `${environment.apiEndPoint}SubLookupMaster/get`;
    public static SubLookupMasterDetail_Api = `${environment.apiEndPoint}SubLookupMaster/get/`;
    public static SubLookupMasterAddUpdate_Api = `${environment.apiEndPoint}SubLookupMaster/Save`;
    public static SubLookupMasterChangeActiveStatus_Api = `${environment.apiEndPoint}SubLookupMaster/ChangeActiveStatus/`;
    public static SubLookupMasterDelete_Api = `${environment.apiEndPoint}SubLookupMaster/delete/`;
    //#endregion




  //#region <<User Setting>>

  public static GetUserProfileApi = `${environment.apiEndPoint}UserSetting/GetUserProfile/`;
  public static UserUpdateProfileApi = `${environment.apiEndPoint}UserSetting/UpdateProfile`;
  public static UserApproveStatusApi = `${environment.apiEndPoint}UserSetting/UpdateApproveStatus/`;
  public static GetUserAvailableAreaApi = `${environment.apiEndPoint}UserSetting/GetUserAvailableAreaForRolebyPinCode/`;
  public static SetUserAvailabilityApi = `${environment.apiEndPoint}UserSetting/SetUserAvailibilty`;
  public static GetUserAvailibiltyListApi = `${environment.apiEndPoint}UserSetting/GetUserAvailibiltyList/`;
  public static DeleteDocumentFileApi = `${environment.apiEndPoint}UserSetting/DeleteDocumentFile/`;


  //#endregion

  //#region  << Product  >>
  public static Product_List_Api = `${environment.apiEndPoint}ProductMaster/GetList`;
  public static Product_AddUpdate_Api = `${environment.apiEndPoint}ProductMaster/Post`;
  public static Product_Delete_Api = `${environment.apiEndPoint}ProductMaster/Delete/`;
  public static Product_ActiveStatus_Api = `${environment.apiEndPoint}ProductMaster/ChangeActiveStatus/`;
  public static Product_Detail_Api = `${environment.apiEndPoint}ProductMaster/Get/`;
  //#endregion


  //#region <<Customer Registration>>
  public static Customer_Registration_Api = `${environment.apiEndPoint}Customer/RegisterCustomer/`;
  // #endregion


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
  static ConfirmUpdate = 'Are you Sure update this record?';
  static DeleteConfirmation = 'Are you want to delete record ?';
  //#endregion
}

export class DropDown_key {

  static ddlState = "ddlState";
  static ddlDistrict = "ddlDistrict";
  static ddlLookupTypeMasters = "ddlLookupTypeMaster"
}
