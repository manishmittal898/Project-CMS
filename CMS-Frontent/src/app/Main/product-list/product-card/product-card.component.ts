import { Component, Input, OnInit } from '@angular/core';
import { ProductMasterViewModel } from 'src/app/Shared/Services/ProductService/product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() Product = {} as ProductMasterViewModel;
  constructor() { }

  ngOnInit(): void {
  }

}
