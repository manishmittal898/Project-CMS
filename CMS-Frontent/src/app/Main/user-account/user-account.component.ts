import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BaseAPIService } from 'src/app/Shared/Services/Core/base-api.service';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {
  pageName = 'My Profile';
  isAuth = false;
  constructor(private readonly _authService: AuthService, private readonly _baseService: BaseAPIService, private readonly _route: Router,) {

    if (_route.url.includes('wishlist')) {
      this.pageName = "Wishlist"
    }
    else if (_route.url.includes('address')) {
      this.pageName = "Address"
    }

  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();

    this._authService.IsAuthentication.subscribe(x => {
      if (x == false) {
        debugger
       // this._route.navigate([this._baseService.Routing_Url.storeUrl])
      }
      this.isAuth = x;
    });
  }

  logout() {
    this._authService.LogOut();
    setTimeout(() => {
            this._route.navigate([this._baseService.Routing_Url.storeUrl]).then(() => {
        window.location.reload();
      });
    }, 100);
  }
}
