import { OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { UserAddressPostModel, UserAddressService, UserAddressViewModel } from '../../../../Shared/Services/UserService/user-address.service';
import { Validators, FormBuilder } from '@angular/forms';
import { DropDown_key } from 'src/app/Shared/Constant';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-save-address',
  templateUrl: './save-address.component.html',
  styleUrls: ['./save-address.component.css']
})
export class SaveAddressComponent implements OnInit, OnChanges {
  @Input() model?= {} as UserAddressPostModel;
  @Output() onSave = new EventEmitter<UserAddressViewModel>();
  @Output() onCancel = new EventEmitter();

  formgrp = this.fb.group({
    FullName: [undefined, Validators.required],
    Mobile: [undefined, Validators.required],
    BuildingNumber: [undefined],
    Address: [undefined, Validators.required],
    PinCode: [undefined, Validators.required],
    Landmark: [undefined],
    City: [undefined, Validators.required],
    StateId: [undefined, Validators.required],
    AddressType: [undefined, Validators.required],
    IsPrimary: [false],

  });
  get ddlkeys() { return DropDown_key };
  dropDown = new DropDownModel();
  get f() { return this.formgrp.controls; }
  constructor(private readonly fb: FormBuilder, public _commonService: CommonService,
    private _userAddressService: UserAddressService, private _toasterService: ToastrService,) {

  }
  ngOnChanges(changes: SimpleChanges): void {


    if (changes && changes['model']) {
      this.getAddress(this.model?.PinCode);
    }
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
      this.f['StateId'].enable();
      this.f['City'].enable();

      this._userAddressService.Save(this.model).subscribe(res => {
        this.f['StateId'].disable();
        this.f['City'].disable();
        if (res.IsSuccess) {
          let resultData = res.Data as UserAddressViewModel;
          resultData.State = this.dropDown.ddlState.find(x => x.Value == this.model.StateId).Text;
          resultData.AddressTypeName = this.dropDown.ddlAddressType.find(x => x.Value == this.model.AddressType).Text;
          resultData.City = this.dropDown.ddlDistrict.find(x => x.Value == this.model.City).Text;
          this.onSave.emit(resultData);
          this.formgrp.reset();
          this._toasterService.success(res.Message as string, 'Success');
        }
        else {
          this._toasterService.error(res.Message as string, 'Failed');

        }
      }, err => {
        this.f['StateId'].disable();
        this.f['City'].disable();
        this._toasterService.error(err.message as string, 'Oops');

      })

    }
  }
  onCancelClick() {
    this.model = {} as UserAddressPostModel;
    this.formgrp.reset();
    this.onCancel.emit()
  }
  getAddress(pinCode:string) {
    this.dropDown.ddlDistrict = [];
    if (pinCode?.length>0) {
      this._userAddressService.getAddressDetailByPinCode(pinCode).subscribe(res => {
        if (res && res[0]?.Status == "Success") {
          const city = res[0]['PostOffice'].map(r => r.District);

          this.dropDown.ddlDistrict = [
            ...city.filter((ele, index, items) =>
              items?.indexOf(ele) === index)?.map(dist => { return { Text: dist, Value: dist } })
          ] ?? [];
          const state = this.dropDown.ddlState.find(st => st.Text == res[0]['PostOffice'][0].State);
          if (state) {
            this.f['StateId'].enable();
            this.model.StateId = state.Value;
            this.f['StateId'].disable();
          }
          if (this.dropDown.ddlDistrict.length == 1) {
            this.f['City'].enable();
            this.model.City = this.dropDown.ddlDistrict[0].Value;
            this.f['City'].disable();
          }
        }
      }, err => {
        this.dropDown.ddlDistrict = [];

      })
    }

  }

}
