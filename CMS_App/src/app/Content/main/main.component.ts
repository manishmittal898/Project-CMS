import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
;
@Component({
  selector: 'app-dashboard',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  @ViewChild('pageBody', { static: false }) appPageBody!: ElementRef;

  constructor( private renderer: Renderer2, private readonly _route: Router) { }

  ngOnInit(): void {

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
