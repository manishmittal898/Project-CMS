import { Component, ElementRef, Renderer2, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Shared/Helper/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  @ViewChild('pageBody', { static: false }) appPageBody!: ElementRef;

  isAuth: boolean = false;
  constructor(public _authService: AuthService, private renderer: Renderer2, private readonly _route: Router) {
    this._authService.IsAuthenticate();
    this._authService.IsAuthentication.subscribe(x => {
      this.isAuth = x;
    });

  }

  setTheme(cssClass: string) {
    if (cssClass == '') {
      var data =
        localStorage.getItem('currentTheme') != null ? localStorage.getItem('currentTheme') : 'theme-default';
      if (data) {
        cssClass = data;
      }
    } else {
      localStorage.setItem('currentTheme', cssClass);
    }


    let el = this.appPageBody.nativeElement;

    let classes = el.className.split(' ');
    let regex = /^b\d$/;
    classes.forEach((cl: string) => {
      if (cl.length > 0) {
        this.renderer.removeClass(el, cl);
      }
    });

    if (cssClass != undefined) {
      this.renderer.addClass(el, cssClass);
    } else {
      this.renderer.addClass(el, 'theme-default');
    }
  }

}
