import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
  pageName = 'My Profile';
  isAuth = false;
  constructor(private readonly _authService: AuthService, private readonly _route: ActivatedRoute) {
    this._route.url.subscribe(r => {
      
    })

  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();

    this._authService.IsAuthentication.subscribe(x => {
      if (x == false) {
        this.logout();
      }
      this.isAuth = x;
    });
  }

  logout() {
    this._authService.LogOut();
  }
}
