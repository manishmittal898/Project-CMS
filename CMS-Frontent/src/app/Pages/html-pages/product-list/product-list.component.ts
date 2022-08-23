import { Component, OnInit } from '@angular/core';
import { Route, ActivatedRoute } from '@angular/router';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  indexModel = new ProductFilterModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService, private readonly _router: ActivatedRoute) {
    if (this._router.snapshot?.queryParams?.id) {
      this.indexModel.CategoryId = [Number(this._router.snapshot.queryParams.id)];
    }
  }

  ngOnInit(): void {
    this.getList();
  }

  onSearch(data) {
    this.indexModel = data;
    this.getList();
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
