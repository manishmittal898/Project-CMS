import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../auth.service';
import { Routing_Url } from '../../Constant';
import { SecurityService } from '../../Services/security.service';
import { AccountService } from '../../Services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  get routing_Url() { return Routing_Url };
  registrationForm: FormGroup;
  get f() { return this.registrationForm.controls; }

  constructor(private readonly _accountService: AccountService, private readonly _security: SecurityService,
    private readonly _route: Router, private readonly toast: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      FirstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      LastName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      Mobile: ['', [Validators.required, Validators.pattern(/^\d{10}$/), this.checkLoginIdExist.bind(this, true)]],
      Email: ['', [Validators.required, Validators.email, this.checkLoginIdExist.bind(this, false)]],
      Password: ['', [Validators.required, Validators.minLength(8)]],
      ConfirmPassword: ['', [Validators.required, this.passwordMatchValidator]]
    });
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
  onSubmit() {
    debugger
    if (this.registrationForm.valid) {
      const frm = {
        FirstName: this.registrationForm.value.FirstName,
        LastName: this.registrationForm.value.LastName,
        Email: this.registrationForm.value.Email,
        Mobile: this.registrationForm.value.Mobile,
        Password: this.registrationForm.value.Password,
      }
      this._accountService.Register(frm).subscribe((res) => {
        if (res.IsSuccess) {
          debugger
          let data = res.Data as any;
          this.toast.success(res.Message?.toString(), 'Registeration');
          this._route.navigate(['/login']);
        } else {
          this.toast.info(res.Message?.toString(), 'Registeration');
        }
      });
    }
  }

  checkLoginIdExist(isMoble: boolean, control) {
    if ((isMoble && control.value.length > 8) || (!isMoble && control.value.length > 5)) {

      this._accountService.CheckUserExist(control.value, isMoble, 0).then
        (x => {
          debugger
          if (x.IsSuccess) {
            if (isMoble) {
              if (x.StatusCode == 205) {
                control.setErrors({ existsMobile: true })
                return { existsMobile: true };

              } else {
                return null;
              }
            } else {
              if (x.StatusCode == 205) {
                control.setErrors({ existsEmail: true })
                return { existsEmail: true };
              }
              else {
                return null;
              }
            }
          }
        })
    } else { return null; }
  }

}
