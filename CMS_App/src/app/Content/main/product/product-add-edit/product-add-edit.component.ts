import { EditorConfig, Message } from './../../../../Shared/Helper/constants';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel, FilterDropDownPostModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ProductImageViewModel, ProductMasterPostModel, ProductMasterViewModel, ProductStockModel } from 'src/app/Shared/Services/product.service';
import { ProductService } from '../../../../Shared/Services/product.service';
import { DropDownItem } from '../../../../Shared/Helper/common-model';

@Component({
  selector: 'app-product-add-edit',
  templateUrl: './product-add-edit.component.html',
  styleUrls: ['./product-add-edit.component.scss']
})
export class ProductAddEditComponent implements OnInit {
  @ViewChild('stockModelPopupClose') stockModelPopup!: ElementRef;
  dropDown = new DropDownModel();
  model = {} as ProductMasterPostModel;
  isFileAttached = false;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    Price: [undefined, Validators.required],
    Caption: [undefined,],
    ViewSection: [undefined,],
    Category: [undefined, Validators.required],
    SubCategory: [undefined],
    Summary: [undefined],
    Description: [undefined],
    ShippingCharge: [undefined],
    Keyword: [undefined],
    MetaTitle: [undefined],
    MetaDesc: [undefined],
    ImagePath: [undefined, Validators.required],
    productFile: [undefined, undefined]

  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' };
  editorConfig = EditorConfig.Config;
  productFile: any;
  ProductFiles: ProductImageViewModel[] = [];
  stockModel = {} as ProductStockModel;
  stockFormGroup = this.fb.group({
    SizeId: [undefined, Validators.required],
    UnitPrice: [undefined, Validators.required],
    Quantity: [undefined, Validators.required],
  });
  get sf() { return this.stockFormGroup.controls; }
  tempStock: ProductStockModel | undefined;
  ddlAvailableProductSize: DropDownItem[] = []
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
  ddlProductSize(): any {

    setTimeout(() => {
      this.ddlAvailableProductSize = this.dropDown?.ddlProductSize?.filter(x => !(this.model?.Stocks?.map(y => { return y.SizeId }))?.includes(Number(x.Value)));
      // let filter = this.dropDown?.ddlProductSize?.filter(x => !(this.model.Stocks.map(y => { return y.SizeId })).includes(Number(x.Value)));
      return this.model?.Stocks?.length > 0 ? this.ddlAvailableProductSize : this.dropDown?.ddlProductSize;
    }, 100);

  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      if (this.model?.Stocks == undefined || this.model?.Stocks?.length === 0) {
        this.toast.warning("Please add atleast 1 stock item...", "Opps");
        return;
      }
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
        this.model.ShippingCharge = data.ShippingCharge ? Number(data.ShippingCharge) : undefined;
        this.model.Keyword = data.Keyword;
        this.model.MetaTitle = data.MetaTitle;
        this.model.MetaDesc = data.MetaDesc;
        this.ProductFiles = data.Files;
        this.model.Stocks = data.Stocks;
      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCaptionTag, DropDown_key.ddlProductViewSection, DropDown_key.ddlProductSize]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlCaptionTag = ddls?.ddlCaptionTag;
        this.dropDown.ddlCategory = ddls?.ddlCategory;
        this.dropDown.ddlProductViewSection = ddls?.ddlProductViewSection;
        this.dropDown.ddlProductSize = ddls?.ddlProductSize;

        this.ddlProductSize();
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
    const ext = fileName?.split('.')[fileName?.split('.').length - 1]?.toLowerCase()??'';
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
  deleteProductFile(id: number) {
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


  onAddStock() {
    this.stockModel = {} as ProductStockModel;
    this.tempStock = undefined;
    this.ddlProductSize();
  }

  onEditStock(stockItem: ProductStockModel, idx: number) {
    this.ddlProductSize()
    if (stockItem && stockItem.Id > 0) {
      idx = this.model.Stocks.findIndex(x => x.Id === stockItem.Id);
    }
    this.stockModel = Object.assign({}, this.model.Stocks[idx]);
    this.tempStock = Object.assign({}, this.stockModel);
    this.model.Stocks.splice(idx, 1);
  }
  onCancelEdit() {
    if (this.tempStock) {
      this.model.Stocks.push(this.tempStock)
    }
  }

  onSaveStock() {

    if (this.model.Stocks && this.model.Stocks.some(x => x.SizeId === this.stockModel.SizeId)) {
      this.stockModel.SizeId = undefined;
      this.toast.warning("Size Already exist, Please Select diffrent size...", "Oops");
      return
    }
    this.stockFormGroup.markAllAsTouched();

    if (this.stockFormGroup.valid) {
      if (!this.model.Stocks) {
        this.model.Stocks = [];
      }
      this.model.Stocks.push(this.stockModel);
      this.stockModel = {} as ProductStockModel;
      this.tempStock = undefined;
      this.stockModelPopup.nativeElement.click();
    }
  }

  applyMainPrice() {

    this.stockModel.UnitPrice = Number(this.model.Price);
  }

  onGetProductSizeLabel(sizeId: any) {
    return this.dropDown.ddlProductSize.find(x => Number(x.Value) === Number(sizeId))?.Text;
  }
}
