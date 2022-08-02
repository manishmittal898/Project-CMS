import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-category-product-list',
  templateUrl: './category-product-list.component.html',
  styleUrls: ['./category-product-list.component.css']
})
export class CategoryProductListComponent implements OnInit {

  constructor() { }
  @Input() CategoryId: number;
  @Input() SubCategoryId: number;


  ngOnInit(): void {
  }

}
