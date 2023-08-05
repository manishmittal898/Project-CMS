import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { AccountService, UserPostModel } from 'src/app/Shared/Services/UserService/account.service';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {
  model = {} as UserPostModel;

  formgrp: FormGroup = this.fb.group({
    FirstName: [undefined, Validators.required],
    LastName: [undefined, Validators.required],
    Mobile: [undefined, Validators.required],
    Email: [undefined, Validators.required],
    Dob: [undefined, Validators.required],
    GenderId: [undefined, Validators.required],
    ProfilePhoto: [undefined, Validators.required]

  });
  get ddlkeys() { return DropDown_key };
  dropDown = new DropDownModel();
  get f() { return this.formgrp.controls; }
  constructor(private _accounntService: AccountService, private _authService: AuthService, private _toasterService: ToastrService,
    private readonly fb: FormBuilder, public _commonService: CommonService,) { }

  ngOnInit(): void {
    this.GetDropDown();
    this.getProfileDetail();
  }

  getProfileDetail() {
    this._accounntService.GetUserDetail().subscribe(res => {
      if (res.IsSuccess) {
        let data = res.Data as UserPostModel;
        this.model.Email = data.Email;
        this.model.FirstName = data.FirstName;
        this.model.LastName = data.LastName;
        this.model.Dob = new Date(data.Dob);
        this.model.Mobile = data.Mobile;
        this.model.ProfilePhoto = data.ProfilePhoto;
        this.model.GenderId = data.GenderId;
      }
    })
  }


  GetDropDown() {

    let serve = this._commonService.GetDropDown([DropDown_key.ddlGender], false).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlGender = ddls?.ddlGender;

      }
    });
  }

  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this._accounntService.UpdateProfile(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this._toasterService.success(res.Message as string, 'Success');

          let data = this._authService.GetUserDetail();
          data.ProfilePhoto = res.Data.ProfilePhoto;
          data.FullName = res.Data.FirstName + ' ' + res.Data.LastName;
          this._authService.SaveUserDetail(data);
        } else {
          this._toasterService.error(res.Message as string, 'Failed');
        }
      }, error => {
        this._toasterService.error(error.message as string, 'Failed');

      })
    }


  }
  onImageChages(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.model.ProfilePhoto = reader.result.toString();
    };

  }
}
