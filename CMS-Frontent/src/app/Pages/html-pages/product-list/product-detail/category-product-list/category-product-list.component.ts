import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';

@Component({
  selector: 'app-category-product-list',
  templateUrl: './category-product-list.component.html',
  styleUrls: ['./category-product-list.component.css']
})
export class CategoryProductListComponent implements OnInit, OnChanges {
  @Input() CategoryId: number;
  @Input() SubCategoryId: number;
  @Input() ExcludeId: number=0;

  indexModel = new ProductFilterModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService) {

    this.indexModel.CategoryId = [Number(this.CategoryId)];

  }
  ngOnChanges(changes: SimpleChanges): void {

    if (changes && changes.CategoryId.currentValue != changes.CategoryId.previousValue) {
      this.getList();
    }
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.

  }

  ngOnInit(): void {
    this.getList();
  }
  getList() {
    if (this.CategoryId > 0) {
      this.indexModel.CategoryId = [Number(this.CategoryId)];
      this._productService.GetList(this.indexModel).subscribe(response => {
        debugger
        if (response.IsSuccess) {
          this.model = response.Data.filter(x => x.Id != this.ExcludeId);
          this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord - (this.ExcludeId > 0 ? 1 : 0) : 0) as number;

        }
      });
    }

  }


}
