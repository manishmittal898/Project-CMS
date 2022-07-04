import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel, FilterDropDownPostModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ProductMasterPostModel, ProductMasterViewModel } from 'src/app/Shared/Services/product.service';
import { ProductService } from '../../../../Shared/Services/product.service';

@Component({
  selector: 'app-product-add-edit',
  templateUrl: './product-add-edit.component.html',
  styleUrls: ['./product-add-edit.component.scss']
})
export class ProductAddEditComponent implements OnInit {
  dropDown = new DropDownModel();
  model = {} as ProductMasterPostModel;
  isFileAttached = false;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    Price: [undefined, Validators.required],
    Caption: [undefined, Validators.required],
    Category: [undefined, Validators.required],
    SubCategory: [undefined, Validators.required],
    Summary: [undefined],
    Description: [undefined],
    ImagePath: [undefined, Validators.required],
  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' }
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService,
    private readonly _productService: ProductService) {
    this._activatedRoute.params.subscribe(x => {
      this.model.Id = Number(this._activatedRoute.snapshot.params.id);
      this.onGetDetail()
    });

  }

  ngOnInit(): void {
    this.GetDropDown();

  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.Price = this.model.Price && this.model.Price > 0 ? Number(this.model.Price) : 0
      this._productService.AddUpdateProductMaster(this.model).subscribe(x => {
        if (x.IsSuccess) {
          this.toast.success("Product added sucessfully...", "Saved");
          this._route.navigate(['./admin/product']);
        } else {
          this.toast.error(x.Message as string, "Faild");
        }
      })

    }
  }
  onGetDetail() {
    this._productService.GetProductMaster(this.model.Id).subscribe(response => {
      if (response.IsSuccess) {
        const data = response.Data as ProductMasterViewModel;
        this.model.Name = data.Name;
        this.model.ImagePath = data.ImagePath;
        this.model.Desc = data.Desc;
        this.model.Price = Number(data.Price);
        this.model.CategoryId = Number(data.CategoryId);
        this.model.SubCategoryId = Number(data.SubCategoryId);
        this.model.CaptionTagId = Number(data.CaptionTagId);
        this.model.Summary = data.Summary;

      } else {

        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCaptionTag]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        debugger
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlCaptionTag = ddls?.ddlCaptionTag;
        this.dropDown.ddlCategory = ddls?.ddlCategory;

      }
    });
  }
  getSubLookUpDropDown(value: number) {

    if (value > 0) {
      const ddlModel = {} as FilterDropDownPostModel;
      ddlModel.FileterFromKey = this.ddlkeys.ddlLookup
      ddlModel.Key = this.ddlkeys.ddlSublookup
      ddlModel.Values = [value]
      this._commonService.GetFilterDropDown(ddlModel).subscribe(x => {
        if (x.IsSuccess) {
          const ddls = x?.Data as DropDownModel;
          this.dropDown.ddlSublookup = ddls.ddlSublookup
        }

      });
    } else {
      this.dropDown.ddlSublookup = [];
    }
  }
  onDocumentAttach(file: FileInfo[]) {
    this.model.ImagePath = file[0].FileBase64;
    this.isFileAttached = true;
  }
  RemoveDocument(file: string) {
    this.model.ImagePath = '';
  }
}
