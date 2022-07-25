import { Component, OnInit } from '@angular/core';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  indexModel = new IndexModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService) { }

  ngOnInit(): void {
    this.getList()
  }

  getList() {
    this._productService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data;
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;
      }
    })
  }

}
