import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-password',
  templateUrl: './user-password.component.html',
  styleUrls: ['./user-password.component.css']
})
export class UserPasswordComponent implements OnInit {
  step = "step1"


  Email = new FormControl('', [Validators.required, Validators.email])
  OTP = new FormControl([undefined, Validators.required])
  Password = new FormControl(['', [Validators.required, Validators.minLength(8)]],)
  ConfirmPassword = new FormControl(['', [Validators.required, this.passwordMatchValidator]])

  constructor(private readonly fb: FormBuilder) { }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.reset();
  }
  reset(): void {
    this.step = "step1"

  }
  sendOTP() {
    this.Email.markAsTouched();
    if (!this.Email.invalid) {

      this.step = 'step2'
    }
  }
  verifyOTP() {
    this.step = 'step3'

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
}
