import { ProductCategoryViewModel, ProductService } from './../../../Shared/Services/product.service';
import { Component, OnInit } from '@angular/core';
import { IndexModel } from 'src/app/Shared/Helper/Common';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  indexModel = new IndexModel();
  model: ProductCategoryViewModel[] = [];
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService) { }

  ngOnInit(): void {
    this.getCategoryList()
  }

  getCategoryList() {
    this._productService.GetCategoryProduct(this.indexModel).subscribe(response => {
      
      if (response.IsSuccess) {

        this.model = response.Data;
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;
      }
    })
  }

}
