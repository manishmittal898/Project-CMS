import { Component, OnInit } from '@angular/core';
import { UserAddressService, UserAddressViewModel } from '../../../Shared/Services/UserService/user-address.service';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';

@Component({
  selector: 'app-my-address',
  templateUrl: './my-address.component.html',
  styleUrls: ['./my-address.component.css']
})
export class MyAddressComponent implements OnInit {
  data = [] as UserAddressViewModel[];
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
  deleteAddress() {

  }
  editAddress() {

  }
}
