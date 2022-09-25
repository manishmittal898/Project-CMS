
import { AngularEditorConfig } from '@kolkov/angular-editor';
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
  public static UserRole_Dropdown_Api = `${environment.apiEndPoint}admin/UserRole/Roles`;
  public static UserRoleList_Api = `${environment.apiEndPoint}admin/UserRole/Get`;
  public static UserRoleDetail_Api = `${environment.apiEndPoint}admin/UserRole/get/`;
  public static UserRoleAddUpdate_Api = `${environment.apiEndPoint}admin/UserRole/post`;
  public static UserRoleCheckRoleExist_Api = `${environment.apiEndPoint}admin/UserRole/CheckRoleExist/`;
  public static UserRoleChangeActiveStatus_Api = `${environment.apiEndPoint}admin/UserRole/ChangeActiveStatus/`;
  public static UserRoleDelete_Api = `${environment.apiEndPoint}admin/UserRole/delete/`;
  //#endregion

  //#region <<LookupTypeMaster>>
  public static LookupTypeMasterList_Api = `${environment.apiEndPoint}admin/LookupTypeMaster/get`;
  public static LookupTypeMasterDetail_Api = `${environment.apiEndPoint}admin/LookupTypeMaster/get/`;
  public static LookupTypeMasterAddUpdate_Api = `${environment.apiEndPoint}admin/LookupTypeMaster/post`;
  public static LookupTypeMasterChangeActiveStatus_Api = `${environment.apiEndPoint}admin/LookupTypeMaster/ChangeActiveStatus/`;
  public static LookupTypeMasterDelete_Api = `${environment.apiEndPoint}admin/LookupTypeMaster/delete/`;
  //#endregion

  //#region <<LookupMaster>>
  public static LookupMasterList_Api = `${environment.apiEndPoint}admin/LookupMaster/get`;
  public static LookupMasterDetail_Api = `${environment.apiEndPoint}admin/LookupMaster/get/`;
  public static LookupMasterAddUpdate_Api = `${environment.apiEndPoint}admin/LookupMaster/Save`;
  public static LookupMasterChangeActiveStatus_Api = `${environment.apiEndPoint}admin/LookupMaster/ChangeActiveStatus/`;
  public static LookupMasterDelete_Api = `${environment.apiEndPoint}admin/LookupMaster/delete/`;
  //#endregion

  //#region <<SubLookupMaster>>
  public static SubLookupMasterList_Api = `${environment.apiEndPoint}admin/SubLookupMaster/get`;
  public static SubLookupMasterDetail_Api = `${environment.apiEndPoint}admin/SubLookupMaster/get/`;
  public static SubLookupMasterAddUpdate_Api = `${environment.apiEndPoint}admin/SubLookupMaster/Save`;
  public static SubLookupMasterChangeActiveStatus_Api = `${environment.apiEndPoint}admin/SubLookupMaster/ChangeActiveStatus/`;
  public static SubLookupMasterDelete_Api = `${environment.apiEndPoint}admin/SubLookupMaster/delete/`;
  //#endregion




  //#region <<User Setting>>

  public static GetUserProfileApi = `${environment.apiEndPoint}admin/UserSetting/GetUserProfile/`;
  public static UserUpdateProfileApi = `${environment.apiEndPoint}admin/UserSetting/UpdateProfile`;
  public static UserApproveStatusApi = `${environment.apiEndPoint}admin/UserSetting/UpdateApproveStatus/`;
  public static GetUserAvailableAreaApi = `${environment.apiEndPoint}admin/UserSetting/GetUserAvailableAreaForRolebyPinCode/`;
  public static SetUserAvailabilityApi = `${environment.apiEndPoint}admin/UserSetting/SetUserAvailibilty`;
  public static GetUserAvailibiltyListApi = `${environment.apiEndPoint}admin/UserSetting/GetUserAvailibiltyList/`;
  public static DeleteDocumentFileApi = `${environment.apiEndPoint}admin/UserSetting/DeleteDocumentFile/`;


  //#endregion

  //#region  << Product  >>
  public static Product_List_Api = `${environment.apiEndPoint}admin/ProductMaster/GetList`;
  public static Product_AddUpdate_Api = `${environment.apiEndPoint}admin/ProductMaster/Post`;
  public static Product_Delete_Api = `${environment.apiEndPoint}admin/ProductMaster/Delete/`;
  public static Product_ActiveStatus_Api = `${environment.apiEndPoint}admin/ProductMaster/ChangeActiveStatus/`;
  public static Product_Detail_Api = `${environment.apiEndPoint}admin/ProductMaster/Get/`;
  public static ProductFile_Delete_Api = `${environment.apiEndPoint}admin/ProductMaster/DeleteProductFile/`;

  //#endregion



  //#region <<CMSPage>>
  public static CMSPageList_Api = `${environment.apiEndPoint}admin/CMSPage/get`;
  public static CMSPageDetail_Api = `${environment.apiEndPoint}admin/CMSPage/get/`;
  public static CMSPageAddUpdate_Api = `${environment.apiEndPoint}admin/CMSPage/Save`;
  public static CMSPageChangeActiveStatus_Api = `${environment.apiEndPoint}admin/CMSPage/ChangeActiveStatus/`;
  public static CMSPageDelete_Api = `${environment.apiEndPoint}admin/CMSPage/delete/`;
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
  static VerifyInput = 'Please Verify input data?';

  //#endregion
}

export class DropDown_key {

  static ddlState = "ddlState";
  static ddlDistrict = "ddlDistrict";
  static ddlLookupTypeMasters = "ddlLookupTypeMaster";
  static ddlCaptionTag = "ddlCaptionTag";
  static ddlCategory = "ddlCategory";
  static ddlProductSize = "ddlProductSize"
  static ddlLookup = "ddllookup"
  static ddlSublookup = "ddlSublookup";
  static ddlSubLookupGroup ="ddlSubLookupGroup";

}

export class EditorConfig {
 static Config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: 'auto',
    minHeight: '25vh',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: 'Enter text here...',
    defaultParagraphSeparator: '',
    defaultFontName: '',
    defaultFontSize: '',
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' }
    ],
    customClasses: [
      {
        name: 'quote',
        class: 'quote',
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: 'titleText',
        class: 'titleText',
        tag: 'h1',
      },
    ],
    sanitize: true,
    toolbarPosition: 'top',
    toolbarHiddenButtons: [
      ['bold', 'italic'],
      ['fontSize']
    ]
  };
}
