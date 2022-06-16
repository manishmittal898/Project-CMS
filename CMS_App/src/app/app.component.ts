import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Shared/Helper/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {


  isAuth: boolean = false;
  constructor(public _authService: AuthService, private renderer: Renderer2, private readonly _route: Router) {
    this._authService.IsAuthenticate();
    this._authService.IsAuthentication.subscribe(x => {
      this.isAuth = x;
    });



  }



}
