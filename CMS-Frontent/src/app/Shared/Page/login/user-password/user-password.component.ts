import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-password',
  templateUrl: './user-password.component.html',
  styleUrls: ['./user-password.component.css']
})
export class UserPasswordComponent implements OnInit {
  step = "step1"
  constructor() { }

  ngOnInit(): void {
  }
  sendOTP() {
    this.step = 'step2'
  }
  verifyOTP() {
    this.step = 'step3'

  }
}
