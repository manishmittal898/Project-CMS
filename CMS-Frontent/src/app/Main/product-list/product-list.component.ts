import { Component, OnInit } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import { SecurityService } from "src/app/Shared/Services/Core/security.service";
import { ProductFilterModel, ProductMasterViewModel, ProductService } from "src/app/Shared/Services/ProductService/product.service";
import { environment } from "src/environments/environment";


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
  constructor(private readonly _productService: ProductService,
    private readonly _router: ActivatedRoute, private readonly _sainitizer: DomSanitizer, private readonly _securityService: SecurityService) {



  }

  ngOnInit(): void {
    this._router.queryParams.subscribe(p => {
      if (p.id) {

        this.indexModel.CategoryId = [this._router.snapshot.queryParams.id];
        this.indexModel.SubCategoryId = [];
      }

      if (p.subid) {
        this.indexModel.SubCategoryId = [this._router.snapshot.queryParams.subid];
      }

      if (this._router.snapshot.params?.name) {
        this.pageName = this._router.snapshot.params?.name.split('_').join(' ');
      }
      setTimeout(() => {
        this.getList();
      }, 100);

    });


  }

  onSearch(data) {
    this.indexModel = data;
    this.getList();
  }

  getList() {
    this._productService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data;
        this.model = this.model; //.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } });
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

      }
    })
  }
  safeURL(dataItem: ProductMasterViewModel) {
    return this._sainitizer.bypassSecurityTrustResourceUrl(`whatsapp://send?text=${environment.sitePath}/collections/${dataItem.Name.split(' ').join('-')}/${dataItem.Id}`);
  }

}
