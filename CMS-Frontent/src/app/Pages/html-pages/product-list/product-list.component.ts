import { Component, OnInit } from '@angular/core';
import{Tooltip} from "node_modules/bootstrap/dist/js/bootstrap.esm.min.js";
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor() { }
  ngOnInit(): void {
    Array.from(document.querySelectorAll('button[data-bs-toggle="tooltip"]'))
    .forEach(tooltipNode => new Tooltip(tooltipNode))
  }

}
