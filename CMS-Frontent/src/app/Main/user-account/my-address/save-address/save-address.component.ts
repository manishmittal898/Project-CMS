import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { UserAddressPostModel, UserAddressService } from '../../../../Shared/Services/UserService/user-address.service';
import { Validators, FormBuilder } from '@angular/forms';
import { DropDown_key } from 'src/app/Shared/Constant';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { DropDownModel } from 'src/app/Shared/Helper/Common';

@Component({
  selector: 'app-save-address',
  templateUrl: './save-address.component.html',
  styleUrls: ['./save-address.component.css']
})
export class SaveAddressComponent implements OnInit {
  @Input() model = {} as UserAddressPostModel;
  @Output() onSave = new EventEmitter<UserAddressPostModel>();
  @Output() onCancel = new EventEmitter();

  formgrp = this.fb.group({
    FullName: [undefined, Validators.required],
    Mobile: [undefined, Validators.required],
    BuildingNumber: [undefined],
    Address: [undefined, Validators.required],
    PinCode: [undefined, Validators.required],
    Landmark: [undefined, Validators.required],
    City: [undefined, Validators.required],
    StateId: [undefined, Validators.required],
    AddressType: [undefined, Validators.required],
    IsPrimary: [false],

  });
  get ddlkeys() { return DropDown_key };
  dropDown = new DropDownModel();
  get f() { return this.formgrp.controls; }
  constructor(private readonly fb: FormBuilder, public _commonService: CommonService, private _userAddressService: UserAddressService) {

  }

  ngOnInit(): void {
    this.GetDropDown();
  }


  GetDropDown() {

    let serve = this._commonService.GetDropDown([DropDown_key.ddlState, DropDown_key.ddlAddressType], false).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlState = ddls?.ddlState;
        this.dropDown.ddlAddressType = ddls?.ddlAddressType;

      }
    });
  }

  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.onSave.emit();
    }
  }

  getAddress(pinCode) {
    this._userAddressService.getAddressDetailByPinCode(pinCode).subscribe(res => {
      if (res?.Status == "Success") {
        const city = res['PostOffice'].map(r => r.District);
        this.dropDown.ddlDistrict = { ...city.filter((item, index) => city.indexOf(item) === index) };
        const state = this.dropDown.ddlState.find(st => st.Text == res['PostOffice'].State);
        this.f['StateId'].enable();
        if (state) {
          this.model.StateId = state.Value;
          this.f['StateId'].disable();
        }
      }

    })
  }

}
