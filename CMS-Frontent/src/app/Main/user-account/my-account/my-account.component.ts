import { Component, OnInit } from '@angular/core';
import { AccountService, UserViewPostModel } from 'src/app/Shared/Services/UserService/account.service';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-my-account',
  templateUrl: './my-account.component.html',
  styleUrls: ['./my-account.component.css']
})
export class MyAccountComponent implements OnInit {
  model = {} as UserViewPostModel;
  constructor(private _accounntService: AccountService, private _authService: AuthService) { }

  ngOnInit(): void {
  }
  getProfileDetail() {
    this._accounntService.GetUserDetail(this._authService.GetUserDetail().UserId).subscribe(res => {

    })
  }

  save() {
    this._accounntService.UpdateProfile(null).subscribe(res => {

    })

  }

}
