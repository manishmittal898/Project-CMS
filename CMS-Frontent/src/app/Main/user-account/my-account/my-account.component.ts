import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel } from 'src/app/Shared/Helper/Common';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { AccountService, UserViewPostModel } from 'src/app/Shared/Services/UserService/account.service';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {
  model = {} as UserViewPostModel;

  formgrp = this.fb.group({
    FirstName: [undefined, Validators.required],
    LastName: [undefined, Validators.required],
    Mobile: [undefined, Validators.required],
    Email: [undefined, Validators.required],
    Dob: [undefined, Validators.required],
    GenderId: [undefined, Validators.required]
  });
  get ddlkeys() { return DropDown_key };
  dropDown = new DropDownModel();
  get f() { return this.formgrp.controls; }
  constructor(private _accounntService: AccountService, private _authService: AuthService,
    private readonly fb: FormBuilder, public _commonService: CommonService,) { }

  ngOnInit(): void {
    this.GetDropDown();
    this.getProfileDetail();
  }

  getProfileDetail() {
    this._accounntService.GetUserDetail(this._authService.GetUserDetail().UserId).subscribe(res => {
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

      })
    }


  }

}
