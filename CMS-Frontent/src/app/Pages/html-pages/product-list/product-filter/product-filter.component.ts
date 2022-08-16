import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel, FilterDropDownPostModel } from 'src/app/Shared/Helper/Common';
import { ProductFilterModel } from 'src/app/Shared/Services/product.service';
import { CommonService } from '../../../../Shared/Services/common.service';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {
  @Input() filterModel: ProductFilterModel = new ProductFilterModel();
  @Output() onFilterChange = new EventEmitter<any>();
  dropDown = new DropDownModel();
  constructor(private _commonService: CommonService) { }

  ngOnInit(): void {
    this.GetDropDown();

  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCaptionTag, DropDown_key.ddlProductSize]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        debugger
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlCaptionTag = ddls?.ddlCaptionTag;
        this.dropDown.ddlCategory = ddls?.ddlCategory;
        this.dropDown.ddlProductSize = ddls?.ddlProductSize;
        this.getSubLookUpDropDown();
      }
    });
  }

  getSubLookUpDropDown() {

  //  if (this.filterModel.CategoryId.length > 0) {
      const ddlModel = {} as FilterDropDownPostModel;
      ddlModel.FileterFromKey = DropDown_key.ddlLookup
      ddlModel.Key = DropDown_key.ddlSubLookupGroup
      ddlModel.Values = [1]//this.filterModel.CategoryId;
      this._commonService.GetFilterDropDown(ddlModel).subscribe(x => {
        if (x.IsSuccess) {
          debugger
          const ddls = x?.Data as DropDownModel;
          this.dropDown.ddlSubLookupGroup = ddls.ddlSubLookupGroup
        }

      });
    // } else {
    //   this.dropDown.ddlSubLookupGroup = [];
    // }
  }

}
