import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel, FilterDropDownPostModel } from 'src/app/Shared/Helper/Common';
import { CommonService } from '../../../Shared/Services/Core/common.service';
import { ProductFilterModel } from 'src/app/Shared/Services/ProductService/product.service';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {
  @Input() filterModel: ProductFilterModel = new ProductFilterModel();
  @Output() onFilterChange = new EventEmitter<ProductFilterModel>();
  dropDown = new DropDownModel();
  get subCategory() {
    return this.dropDown?.ddlSubLookupGroup?.filter(x => this.filterModel.CategoryId.includes(x.CategoryId)) ?? [];
  }
  get maxPrice() {
    return this.dropDown?.ddlProductPrice?.Value ?? 10000;
  }
  constructor(private _commonService: CommonService) { }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCaptionTag, DropDown_key.ddlProductSize, DropDown_key.ddlSubLookupGroup, DropDown_key.ddlProductPrice], true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlCaptionTag = ddls?.ddlCaptionTag;
        this.dropDown.ddlCategory = ddls?.ddlCategory;
        this.dropDown.ddlProductSize = ddls?.ddlProductSize;
        this.dropDown.ddlSubLookupGroup = ddls.ddlSubLookupGroup
        this.dropDown.ddlProductPrice = ddls.ddlProductPrice

        if (this.dropDown.ddlProductPrice.Value > 1) {
          this.filterModel.Price[1] = Object.assign(this.dropDown.ddlProductPrice.Value)
        }
        //  this.getSubLookUpDropDown();
        this.applyFilter();
      }
    });
  }

  getSubLookUpDropDown() {
    //if (this.filterModel.CategoryId.length > 0) {
    this.dropDown.ddlSubLookupGroup = [];
    const ddlModel = {} as FilterDropDownPostModel;
    ddlModel.FileterFromKey = DropDown_key.ddlLookup
    ddlModel.Key = DropDown_key.ddlSubLookupGroup
    ddlModel.Values = this.filterModel.CategoryId.length > 0 ? this.filterModel.CategoryId : [];
    this._commonService.GetFilterDropDown(ddlModel).subscribe(x => {
      if (x.IsSuccess) {
        const ddls = x?.Data as DropDownModel;
        this.dropDown.ddlSubLookupGroup = ddls.ddlSubLookupGroup
      }

    });
    // } else {
    //   this.dropDown.ddlSubLookupGroup = [];
    // }
  }
  updateSubCategoryData() {
    let j = [];
    this.subCategory?.forEach(x => x?.Data?.forEach(s => { j.push(s.Value) }));
    this.filterModel.SubCategoryId = this.filterModel?.SubCategoryId?.filter(x => j.includes(x)) ?? [];
  }

  applyFilter() {
    this.onFilterChange.emit(this.filterModel);
  }

}
