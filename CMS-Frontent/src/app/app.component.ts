import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CMS-Frontent';



  constructor() { }
  ngOnInit(): void {

  }



}
