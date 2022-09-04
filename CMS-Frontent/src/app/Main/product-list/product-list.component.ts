import { DomSanitizer } from '@angular/platform-browser';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from './../../Shared/Services/product.service';
import { Component, OnInit } from '@angular/core';
import { Route, ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  pageName = 'Store'
  indexModel = new ProductFilterModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService, private readonly _router: ActivatedRoute, private readonly _sainitizer: DomSanitizer) {



  }

  ngOnInit(): void {
    this._router.queryParams.subscribe(p => {
      if (p.id) {
        this.indexModel.CategoryId = [Number(this._router.snapshot.queryParams.id)];
        this.indexModel.SubCategoryId = null;
      }
      if (p.subid) {
        this.indexModel.SubCategoryId = [Number(this._router.snapshot.queryParams.subid)];
      }

      if (this._router.snapshot.params?.name) {
        this.pageName = this._router.snapshot.params?.name.split('_').join(' ');
      }
      this.getList();

    }); // you can also do this in ngOnInit


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
  safeURL(dataItem: ProductMasterViewModel) {
    return this._sainitizer.bypassSecurityTrustResourceUrl(`whatsapp://send?text=${environment.sitePath}/store/${dataItem.Name.split(' ').join('-')}/${dataItem.Id}`);
  }

}
