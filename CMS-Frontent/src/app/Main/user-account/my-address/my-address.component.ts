import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserAddressService, UserAddressViewModel, UserAddressPostModel } from '../../../Shared/Services/UserService/user-address.service';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';

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
  constructor(private _userAddressService: UserAddressService, private _securityService: SecurityService) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {

    const model = new IndexModel()
    model.PageSize = 101;
    this._userAddressService.GetList(model).subscribe(res => {
      if (res.IsSuccess) {
        this.data = res.Data.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } })
      }
    })
  }
  addAddress() {
    this.selectedData = {} as UserAddressPostModel;
    debugger
    this.btnShow.nativeElement.click();

  }
  editAddress(address: UserAddressViewModel) {
    this.selectedData = address;
    this.btnShow.nativeElement.click();

  }
  deleteAddress() {
  }
  closePopup() {
    this.btnClose.nativeElement.click();
  }
  onSave(data) {
    this.closePopup();
  }

}
