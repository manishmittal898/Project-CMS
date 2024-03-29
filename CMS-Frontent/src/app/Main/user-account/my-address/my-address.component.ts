import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
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
  @Output() onSelect = new EventEmitter<UserAddressViewModel>();
  data : UserAddressViewModel[];
  selectedData = {} as UserAddressPostModel;
  selectedAddress = {} as UserAddressViewModel;
  showSavePopup = false;
  isAdd = true;
  isLoading=true;
  @Input() config = new Config()
  constructor(private _userAddressService: UserAddressService,
    private _securityService: SecurityService, private _toasterService: ToastrService,
    private _commonService: CommonService) {
      this.getData();
    }

  ngOnInit(): void { }

  getData() {
    this.isLoading=true;
    const model = new IndexModel();
    model.PageSize = 101;
    this._userAddressService.GetList(model).subscribe(res => {
      if (res.IsSuccess) {
        this.isLoading=false;
        this.data = res.Data;//.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } })
        this.onSelected(this.data.find(x => x.IsPrimary));
      }
    })
  }

  addAddress() {
    this.selectedData = {} as UserAddressPostModel;
    this.isAdd = true;
    this.showSavePopup = true;
    setTimeout(() => {
      this.btnShow.nativeElement.click();
    }, 100);
  }

  editAddress(address: UserAddressViewModel) {
    this.selectedData = address;
    this.showSavePopup = true;
    this.isAdd = false;
    setTimeout(() => {
      this.btnShow.nativeElement.click();
    }, 100);
  }

  deleteAddress(address) {
    this._commonService.Question(Message.DeleteConfirmation).then(result => {
      if (result) {
        this._userAddressService.Delete(address.Id).subscribe(res => {
          if (res.IsSuccess) {
            this._toasterService.success(res.Message as string, 'Success');
            let indx = this.data.findIndex(x => x.Id == address.Id)
            this.data.splice(indx, 1);
          } else {
            this._toasterService.error(res.Message as string, 'Oops');
          }
        })
      }
    }, err => {
      this._toasterService.error(err.message as string, 'Oops');

    })
  }

  setPrimaryeAddress(address: UserAddressViewModel) {
    if (this.data.length > 0) {
      this._commonService.Question(Message.ConfirmUpdate).then(result => {
        if (result) {
          this._userAddressService.SetPrimary(address.Id).subscribe(res => {
            if (res.IsSuccess) {
              this._toasterService.success(res.Message as string, 'Success');
              this.data.forEach(x => {
                x.IsPrimary = x.Id == address.Id ? !x.IsPrimary : false;
              }
              )
            } else {
              this._toasterService.error(res.Message as string, 'Failed');
            }
          },
            err => {
              this._toasterService.error(err.message as string, 'Internal error');

            }
          )
        }
      })
    }

  }

  closePopup() {
    this.selectedData = {} as UserAddressPostModel;
    this.btnClose?.nativeElement?.click();
    this.showSavePopup = false;
  }

  onSave(data: UserAddressViewModel) {

    if (data.IsPrimary) {
      this.data.forEach(ele => {
        ele.IsPrimary = false;
      })
    }
    if (this.isAdd) {
      this.data.push(data);
    } else {
      let ind = this.data.findIndex(res => res.Id == data.Id);
      this.data[ind] = data;
    }
    this.onSelected(this.data.find(x => x.IsPrimary));
    this.closePopup();
  }
  onSelected(address) {
    this.selectedAddress = address;
    this.onSelect.emit(this.selectedAddress)
  }

}
export class Config {
  isDeleteButton = true;

}
