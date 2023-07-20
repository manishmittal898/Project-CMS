import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserAddressService, UserAddressViewModel, UserAddressPostModel } from '../../../Shared/Services/UserService/user-address.service';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';
import { Message } from 'src/app/Shared/Constant';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';

@Component({
  selector: 'app-my-address',
  templateUrl: './my-address.component.html',
  styleUrls: ['./my-address.component.css']
})
export class MyAddressComponent implements OnInit {
  @ViewChild('btnShow') btnShow: ElementRef;
  @ViewChild('btnClose') btnClose: ElementRef;
  data = [] as UserAddressViewModel[];
  selectedData = {} as UserAddressPostModel;
  constructor(private _userAddressService: UserAddressService,
    private _securityService: SecurityService, private _toasterService: ToastrService,
    private _commonService: CommonService) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {

    const model = new IndexModel();
    model.PageSize = 101;
    this._userAddressService.GetList(model).subscribe(res => {
      if (res.IsSuccess) {
        this.data = res.Data.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } })
      }
    })
  }

  addAddress() {
    this.selectedData = {} as UserAddressPostModel;
    this.btnShow.nativeElement.click();
  }

  editAddress(address: UserAddressViewModel) {
    this.selectedData = address;
    this.btnShow.nativeElement.click();
  }

  deleteAddress(address) {
    this._commonService.Question(Message.DeleteConfirmation).then(result => {
      if (result) {
        this._userAddressService.Delete(address.Id).subscribe(res => {
          if (res.IsSuccess) {
            this._toasterService.success(res.Message as string, 'Success');
          } else {
            this._toasterService.error(res.Message as string, 'Oops');
          }
        })
      }
    })
  }

  setPrimaryeAddress(address) {
    this._commonService.Question(Message.ConfirmUpdate).then(result => {
      if (result) {
        this._userAddressService.SetPrimary(address.Id).subscribe(res => {
          if (res.IsSuccess) {
            this._toasterService.success(res.Message as string, 'Success');
          } else {
            this._toasterService.error(res.Message as string, 'Oops');
          }
        })
      }
    })
  }

  closePopup() {
    this.btnClose.nativeElement.click();
  }

  onSave(data) {
    this.closePopup();
  }

}
