import { ChangePasswordModel } from './../../../Services/UserService/account.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';
import { OTPService, OTPModel } from '../../../Services/UserService/otp.service';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/Shared/Services/UserService/account.service';
import { interval, timer } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';

@Component({
  selector: 'app-user-password',
  templateUrl: './user-password.component.html',
  styleUrls: ['./user-password.component.css']
})
export class UserPasswordComponent implements OnInit {
  step = "step1"
  sessionId: string;
  Email = new FormControl('', [Validators.required, Validators.email]);
  OTPFrm = this.fb.group({
    input1: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
    input2: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
    input3: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
    input4: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
    input5: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
    input6: ['', [Validators.required, Validators.maxLength(1), Validators.minLength(1)]],
  })


  PasswordFrm = this.fb.group({
    Password: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50), this.passwordValidator.bind(this)]],
    ConfirmPassword: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(50), this.passwordMatchValidator]]
  })
  get OTPctrl() { return this.OTPFrm.controls }
  get PasswordCtrl() { return this.PasswordFrm.controls }
  isLoading = false;
  @ViewChild("btnClose") btnClose: ElementRef;

  timeCounter = 180;
  isShowPass=false;
  isCShowPass=false;
  constructor(private readonly fb: FormBuilder, private readonly _otpService: OTPService, public readonly _commonService: CommonService,
    private readonly toasterService: ToastrService, private readonly _accountService: AccountService) { }

  ngOnInit(): void {
    //  this.startTimer()
  }
  ngOnDestroy(): void {
    this.reset();
  }
  reset(): void {
    this.step = "step1"
    this.Email.reset();
    this.OTPFrm.reset();
    this.PasswordFrm.reset();

    this.timeCounter = 180;
  }
  sendOTP() {
    this.Email.markAsTouched();
    if (this.Email.valid) {
      this.isLoading = true;
      this.timeCounter = 180;
      this._otpService.GetOTP(this.Email.value).subscribe(res => {
        this.isLoading = false;
        if (res.IsSuccess) {

          this.sessionId = res.Data;
          this.step = 'step2'
          this.toasterService.success(res.Message as string, 'Success');
          this.startTimer();
        } else {
          this.toasterService.warning(res.Message as string, 'Failed');
        }
      }, err => {
        this.isLoading = false;
        this.toasterService.error(err.message as string, 'Oops');
      })
    }
  }
  verifyOTP() {
    this.OTPFrm.markAllAsTouched();
    if (this.OTPFrm.valid) {
      const otp = {} as OTPModel;
      otp.OTP = `${this.OTPctrl.input1.value.trim()}${this.OTPctrl.input2.value.trim()}${this.OTPctrl.input3.value.trim()}${this.OTPctrl.input4.value.trim()}${this.OTPctrl.input5.value.trim()}${this.OTPctrl.input6.value.trim()}`
      otp.SessionId = this.sessionId;
      this.isLoading = true;
      this._otpService.VerifyOTP(otp).subscribe(res => {
        this.isLoading = false;
        if (res.IsSuccess) {
          this.step = 'step3'
          this.toasterService.success(res.Message as string, 'Success');

        } else {
          this.toasterService.warning(res.Message as string, 'Failed');
        }
      }, err => {
        this.isLoading = false;
        this.toasterService.error(err.message as string, 'Oops');
      })
    }

  }

  onPasswordSubmit() {
    if (this.PasswordFrm.valid) {

      const model = {} as ChangePasswordModel;
      model.Email = this.Email.value;
      model.OTP = `${this.OTPctrl.input1.value.trim()}${this.OTPctrl.input2.value.trim()}${this.OTPctrl.input3.value.trim()}${this.OTPctrl.input4.value.trim()}${this.OTPctrl.input5.value.trim()}${this.OTPctrl.input6.value.trim()}`;
      model.SessionID = this.sessionId;
      model.Password = this.PasswordCtrl.Password.value;
      this.isLoading = true;
      this._accountService.ChangePassword(model).subscribe(res => {
        this.isLoading = false;
        if (res.IsSuccess) {
          this.toasterService.success(res.Message as string, 'Success');
          this.btnClose.nativeElement.click();

        } else {
          this.toasterService.warning(res.Message as string, 'Failed');
        }
      }, err => {
        this.isLoading = false;
        this.toasterService.error(err.message as string, 'Oops');
      })

    }
  }
  passwordMatchValidator(control: AbstractControl) {
    if (control && control.value !== undefined && control.value !== null && control.value !== '') {
      const confirmPassword = control.value;
      const passwordControl = control.root.get('Password');

      if (passwordControl) {
        const password = passwordControl.value;
        if (confirmPassword !== password) {
          return { notMatched: true };
        }
      }
    }
    return null;


  }
  passwordValidator(control: AbstractControl) {

    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,50}$/;

    const isValid = passwordRegex.test(control.value);

    if (!isValid) {
      return { invalidPassword: true };
    }

    return null;
  }
  private startTimer() {
    const timerCount = interval(1000).pipe(
      take(181)
    );

    timerCount.subscribe(rs => {
      this.timeCounter = (180 - rs);

    });
  }
  processKeyUp(e, el) {

    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)) {
      el.focus();
    }
  }

}
