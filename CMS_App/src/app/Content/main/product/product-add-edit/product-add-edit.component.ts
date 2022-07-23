import { EditorConfig, Message } from './../../../../Shared/Helper/constants';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel, FilterDropDownPostModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ProductImageViewModel, ProductMasterPostModel, ProductMasterViewModel } from 'src/app/Shared/Services/product.service';
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
    Caption: [undefined,],
    Category: [undefined, Validators.required],
    SubCategory: [undefined],
    Summary: [undefined],
    Description: [undefined],
    ShippingCharge: [undefined],
    Keyword: [undefined],
    ImagePath: [undefined, Validators.required],
    productFile: [undefined, undefined]
  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' };
  editorConfig = EditorConfig.Config;
  productFile: any;
  ProductFiles: ProductImageViewModel[] = [];

  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService,
    private readonly _productService: ProductService) {
    this._activatedRoute.params.subscribe(x => {
      this.model.Id = this._activatedRoute.snapshot.params.id ? Number(this._activatedRoute.snapshot.params.id) : 0;
      if (this.model.Id > 0) {
        this.onGetDetail();
      }
    });

  }

  ngOnInit(): void {
    this.GetDropDown();

  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.Price = this.model.Price && this.model.Price > 0 ? Number(this.model.Price) : 0;
      this.model.ShippingCharge = this.model.ShippingCharge && this.model.ShippingCharge > 0 ? Number(this.model.ShippingCharge) : 0

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
        this.model.Price = data.Price ? Number(data.Price) : undefined;
        this.model.CategoryId = data.CategoryId ? Number(data.CategoryId) : undefined;
        this.model.SubCategoryId = data.SubCategoryId ? Number(data.SubCategoryId) : undefined;
        this.model.CaptionTagId = data.CaptionTagId ? Number(data.CaptionTagId) : undefined;
        this.model.Summary = data.Summary;
        this.model.ShippingCharge =  data.ShippingCharge ? Number(data.ShippingCharge) : undefined;
        this.model.Keyword = data.Keyword;
        this.ProductFiles = data.Files;
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
  onProductFileAttach(file: FileInfo[]) {
    debugger
    if (file.length > 0) {
      if (!this.model.Files) {
        this.model.Files = [];
      }
      this.model.Files = file.map(x => { return x.FileBase64 });
      this.productFile = undefined
    } else {
      this.model.Files = [];
    }
  }
  getFileType(fileName: string) {
    const ext = fileName.split('.')[fileName.split('.').length - 1].toLowerCase();
    if (['doc', 'docx', 'ppt', 'pptx', 'pdf', 'txt', 'xlx', 'xlsx'].some(x => x.toLowerCase() === ext)) {
      return 'doc';
    } else if (['jpeg', 'gif', 'png', 'jpg', 'svg'].some(x => x.toLowerCase() === ext)) {
      return 'image';
    }
    else if (['mp4', 'mkv', 'avi',].some(x => x.toLowerCase() === ext)) {
      return 'video';
    } else {
      return ext;
    }

  }
  deleteProdcutFile(id: number) {
    this._commonService.Question(Message.DeleteConfirmation as string).then(result => {
      if (result) {
        let subscription = this._productService.DeleteProductFile(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.ProductFiles.findIndex(x => x.Id == id);
              this.ProductFiles.splice(idx, 1);
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }
}
