import { Component, OnInit } from '@angular/core';
import{Tooltip} from "node_modules/bootstrap/dist/js/bootstrap.esm.min.js";
@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CMS-Frontent';



  constructor() { }
  ngOnInit(): void {
    Array.from(document.querySelectorAll('button[data-bs-toggle="tooltip"]'))
    .forEach(tooltipNode => new Tooltip(tooltipNode))
  }



}
