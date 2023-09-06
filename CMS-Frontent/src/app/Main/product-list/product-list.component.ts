import { Component, OnInit } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute, Router } from "@angular/router";
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
  sortValue: string = "CreatedOn_false";

  constructor(private readonly _productService: ProductService,
    private readonly _router: ActivatedRoute, private _route: Router, private readonly _sainitizer: DomSanitizer, private readonly _securityService: SecurityService) {



  }

  ngOnInit(): void {
    this._router.queryParams.subscribe(p => {
      if (p.id) {
        this.indexModel.CategoryId = this._router.snapshot.queryParams.id.split(',');
        this.indexModel.SubCategoryId = [];
      }
      if (p.subid) {
        this.indexModel.SubCategoryId = this._router.snapshot.queryParams.subid.split(',');
      }
      if (p.prc) {
        this.indexModel.Price = this._router.snapshot.queryParams.prc.split(',').map(x => Number(x));
      }
      if (p.clr) {
        this.indexModel.ColorId = p.clr.split(',');
      }
      if (p.dscnt) {
        this.indexModel.DiscountId = p.dscnt.split(',');
      }
      if (p.ocsn) {
        this.indexModel.OccasionId = p.ocsn.split(',');
      }
      if (p.ptrn) {
        this.indexModel.PatternId = p.ptrn.split(',');
      }
      if (p.lngth) {
        this.indexModel.LengthId = p.lngth.split(',');
      }
      if (p.fbrc) {
        this.indexModel.FabricId = p.fbrc.split(',');
      }
      if (p.sz) {
        this.indexModel.SizeId = p.sz.split(',');
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
    let query = {};
    if (this.indexModel?.CategoryId?.length > 0) {
      query['id'] = this.indexModel.CategoryId.join(',');
    }
    if (this.indexModel?.SubCategoryId?.length > 0) {

      query['subid'] = this.indexModel.SubCategoryId.join(',');
    }
    if (this.indexModel?.ColorId?.length > 0) {

      query['clr'] = this.indexModel.ColorId.join(',');
    }
    if (this.indexModel?.DiscountId?.length > 0) {

      query['dscnt'] = this.indexModel.DiscountId.join(',');
    }
    if (this.indexModel?.OccasionId?.length > 0) {

      query['ocsn'] = this.indexModel.OccasionId.join(',');
    }
    if (this.indexModel?.PatternId?.length > 0) {

      query['ptrn'] = this.indexModel.PatternId.join(',');
    }
    if (this.indexModel?.LengthId?.length > 0) {

      query['lngth'] = this.indexModel.LengthId.join(',');
    }
    if (this.indexModel?.FabricId?.length > 0) {

      query['fbrc'] = this.indexModel.FabricId.join(',');
    }
    if (this.indexModel?.SizeId?.length > 0) {

      query['sz'] = this.indexModel.SizeId.join(',');
    }
    if (this.indexModel?.Price?.length > 0) {
      query['prc'] = this.indexModel.Price.join(',');
    }
    this._route.navigate(
      [],  // Remain on current route
      {
        relativeTo: this._router,
        queryParams: query,
        replaceUrl: true,
        skipLocationChange: false
      });
    // this.getList();
  }

  getList() {
    let sModel = this.sortValue.split('_');//as { column: string, OrderByAsc: boolean }
    this.indexModel.OrderBy = sModel[0];
    this.indexModel.OrderByAsc = sModel[1] == 'true' ? true : false;

    this._productService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data;
        // this.model = this.model; //.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } });
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

      }
    })
  }
  safeURL(dataItem: ProductMasterViewModel) {
    return this._sainitizer.bypassSecurityTrustResourceUrl(`whatsapp://send?text=${environment.sitePath}/collections/${dataItem.Name.split(' ').join('-')}/${dataItem.Id}`);
  }

}
