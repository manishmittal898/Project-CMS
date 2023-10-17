import { EditorConfig, Message } from './../../../../Shared/Helper/constants';
import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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
    UniqueID: [undefined, Validators.required],
    Name: [undefined, Validators.required],
    Price: [undefined, [Validators.required, this.minValueValidator.bind(this)]],
    SellingPrice: [undefined, this.minValueValidator.bind(this)],
    Caption: [undefined,],
    ViewSection: [undefined,],
    Category: [undefined, Validators.required],
    SubCategory: [undefined],
    Occasion: [undefined],
    Fabric: [undefined],
    Length: [undefined],
    Color: [undefined],
    Pattern: [undefined],
    Summary: [undefined],
    Description: [undefined],
    Keyword: [undefined],
    MetaTitle: [undefined],
    MetaDesc: [undefined],
    ImagePath: [undefined, Validators.required],
    productFile: [undefined, undefined]

  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp?.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' };
  editorConfig = EditorConfig.Config;
  productFile: any;
  ProductFiles: ProductImageViewModel[] = [];
  stockModel = {} as ProductStockModel;
  stockFormGroup = this.fb.group({
    SizeId: [undefined, Validators.required],
    UnitPrice: [undefined, [Validators.required, this.minStockValueValidator.bind(this)]],
    SellingPrice: [undefined, [Validators.required, this.minStockValueValidator.bind(this)]],
    Quantity: [undefined, Validators.required],
  });

  get sf() { return this.stockFormGroup?.controls; }
  tempStock: ProductStockModel | undefined;
  ddlAvailableProductSize: DropDownItem[] = [];
  @Input() set Id(value: string) {
    this.model.Id = value;
    if (this.model !== null && this.model.Id.length > 0) {
      this.onGetDetail();
    } else {
      this.reset();
    }
  }

  @Output() OnSave = new EventEmitter<{ status: boolean, recordId: string }>();
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService,
    private readonly _productService: ProductService) {
    this._activatedRoute.params.subscribe(x => {
      this.model.Id = this._activatedRoute.snapshot.params.id ? this._activatedRoute.snapshot.params.id : null;
      if (this.model.Id !== null) {
        this.onGetDetail();
      }
    });

  }

  minValueValidator(ctrl: AbstractControl): ValidationErrors | null {
    const val = this?.f != null && this?.f.SellingPrice?.value != undefined ? Number(this?.f?.SellingPrice?.value) : 0;
    const price = this?.f != null && this?.f.Price?.value != undefined ? Number(this?.f?.Price?.value) : 0;

    if (Number(price) < Number(val)) {
      return {
        minValue: true
      }
    }
    return null;
  }
  checkSellingPrice() {
    const val = this?.f != null && this?.f.SellingPrice?.value != undefined ? Number(this?.f?.SellingPrice?.value) : 0;
    const price = this?.f != null && this?.f.Price?.value != undefined ? Number(this?.f?.Price?.value) : 0;

    if (val > price) {
      this.f.SellingPrice.setValue(price);
    }
    else if (price > 0 && val == 0) {
      this.f.SellingPrice.setValue(price);
    }
  }
  minStockValueValidator(ctrl: AbstractControl): ValidationErrors | null {
    const val = this?.sf != null && this?.sf.SellingPrice?.value != undefined ? Number(this?.sf?.SellingPrice?.value) : 0;
    const price = this?.sf != null && this?.sf?.UnitPrice?.value != undefined ? Number(this?.sf?.UnitPrice?.value) : 0;
    if (val > price) {
      this?.sf?.SellingPrice?.setValue(price);
      return null;
    }
    if (Number(price) < Number(val)) {
      return {
        minValue: true
      }
    }
    return null;
  }
  checkStockSellingPrice() {
    const val = this?.sf != null && this?.sf.SellingPrice?.value != undefined ? Number(this?.sf?.SellingPrice?.value) : 0;
    const price = this?.sf != null && this?.sf.UnitPrice?.value != undefined ? Number(this?.sf?.UnitPrice?.value) : 0;
    if (val > price) {
      this?.sf?.SellingPrice?.setValue(price);
    } else if (price > 0 && val == 0) {
      this?.sf?.SellingPrice?.setValue(price);
    }
  }
  ngOnInit(): void {
    this.GetDropDown();
  }

  reset() {
    this.model = {} as ProductMasterPostModel;
    this.isFileAttached = false;
    this.productFile = undefined
    this.ProductFiles = [];
    this.stockModel = {} as ProductStockModel;
    this.formgrp.reset();
    this.stockFormGroup.reset()
  }
  ddlProductSize(): any {

    setTimeout(() => {
      this.ddlAvailableProductSize = this.dropDown?.ddlProductSize?.filter(x => !(this.model?.Stocks?.map(y => { return y.SizeId }))?.includes(x.Value));
      // let filter = this.dropDown?.ddlProductSize?.filter(x => !(this.model.Stocks.map(y => { return y.SizeId })).includes(Number(x.Value)));
      return this.model?.Stocks?.length > 0 ? this.ddlAvailableProductSize : this.dropDown?.ddlProductSize;
    }, 10);

  }

  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      if (this.model?.Stocks == undefined || this.model?.Stocks?.length === 0) {
        this.toast.warning("Please add atleast 1 stock item...", "Opps");
        return;
      }
      this.model.Price = this.model.Price && this.model.Price > 0 ? Number(this.model.Price) : 0;
      this.model.SellingPrice = this.model.SellingPrice && this.model.SellingPrice > 0 ? Number(this.model.SellingPrice) : 0;

      this.model.Stocks.forEach(x => {
        x.Discount = x.Discount && x.Discount > 0 ? Number(x.Discount) : 0;
        x.UnitPrice = x.UnitPrice && x.UnitPrice > 0 ? Number(x.UnitPrice) : 0;
        x.SellingPrice = x.SellingPrice && x.SellingPrice > 0 ? Number(x.SellingPrice) : 0;
      })
      // this.model.ShippingCharge = this.model.ShippingCharge && this.model.ShippingCharge > 0 ? Number(this.model.ShippingCharge) : 0

      this._productService.AddUpdateProductMaster(this.model).subscribe(x => {
        if (x.IsSuccess) {
          this.toast.success("Product added successfully...", "Saved");
          this._route.navigate(['./admin/product']);
          this.OnSave.emit({ status: true, recordId: x.Data as string });
        } else {
          this.OnSave.emit({ status: true, recordId: x.Data as string });
          this.toast.error(x.Message as string, "Failed");
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
        this.model.SellingPrice = Object.assign((data.SellingPrice ? Number(data.SellingPrice) : data.Price), 0);
        this.model.CategoryId = data.CategoryId ? data.CategoryId : undefined;
        this.model.SubCategoryId = data.SubCategoryId ? data.SubCategoryId : undefined;
        this.model.CaptionTagId = data.CaptionTagId ? data.CaptionTagId : undefined;
        this.model.Summary = data.Summary;
        this.model.Discount = data.Discount;
        this.model.OccasionId = data.OccasionId;
        this.model.FabricId = data.FabricId;
        this.model.LengthId = data.LengthId;
        this.model.ColorId = data.ColorId;
        this.model.PatternId = data.PatternId;
        this.model.UniqueId = data.UniqueId;
        this.model.Keyword = data.Keyword;
        this.model.MetaTitle = data.MetaTitle;
        this.model.MetaDesc = data.MetaDesc;
        this.ProductFiles = data.Files;
        this.model.Stocks = data.Stocks;
        this.model.ViewSectionId = data.ViewSectionId ? data.ViewSectionId : undefined;;

      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCaptionTag,
    DropDown_key.ddlProductViewSection, DropDown_key.ddlProductSize, DropDown_key.ddlProductDiscount,
    DropDown_key.ddlProductOccasion, DropDown_key.ddlProductFabric, DropDown_key.ddlProductLength,
    DropDown_key.ddlProductColor, DropDown_key.ddlProductPattern]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlCaptionTag = ddls?.ddlCaptionTag;
        this.dropDown.ddlCategory = ddls?.ddlCategory;
        this.dropDown.ddlProductViewSection = ddls?.ddlProductViewSection;
        this.dropDown.ddlProductSize = ddls?.ddlProductSize;
        this.dropDown.ddlProductDiscount = ddls?.ddlProductDiscount;
        this.dropDown.ddlProductOccasion = ddls?.ddlProductOccasion;
        this.dropDown.ddlProductFabric = ddls?.ddlProductFabric;
        this.dropDown.ddlProductLength = ddls?.ddlProductLength;
        this.dropDown.ddlProductColor = ddls?.ddlProductColor;
        this.dropDown.ddlProductPattern = ddls?.ddlProductPattern;
        this.ddlProductSize();
      }
    });
  }
  getSubLookUpDropDown(value: string) {

    if (value?.length > 0) {
      const ddlModel = {} as FilterDropDownPostModel;
      ddlModel.FileterFromKey = this.ddlkeys.ddlLookup
      ddlModel.Key = this.ddlkeys.ddlSublookup
      ddlModel.Values = [value]
      this._commonService.GetFilterDropDown(ddlModel).subscribe(x => {
        if (x.IsSuccess) {
          const ddls = x?.Data as DropDownModel;
          this.dropDown.ddlSublookup = ddls.ddlSublookup
        }
        if (this.dropDown.ddlSublookup.findIndex(x => x.Value == this.model.SubCategoryId) == -1) {
          this.model.SubCategoryId = undefined;
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
    const ext = fileName?.split('.')[fileName?.split('.').length - 1]?.toLowerCase() ?? '';
    if (['doc', 'docx', 'ppt', 'pptx', 'pdf', 'txt', 'xlx', 'xlsx'].some(x => x.toLowerCase() === ext)) {
      return 'doc';
    } else if (['jpeg', 'gif', 'png', 'jpg', 'svg', 'webp'].some(x => x.toLowerCase() === ext)) {
      return 'image';
    }
    else if (['mp4', 'mkv', 'avi',].some(x => x.toLowerCase() === ext)) {
      return 'video';
    } else {
      return ext;
    }

  }
  deleteProductFile(id: string) {
    this._commonService.Question(Message.DeleteConfirmation as string).then(result => {
      if (result) {
        let subscription = this._productService.DeleteProductFile(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, "Delete");
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
  getDiscountValue(price = 0, sellingPrice = 0) {

    if (price == sellingPrice) {
      return 0;
    } else if (sellingPrice == 0) {
      return 100;
    }
    var ddValue = ((price - sellingPrice) / price) * 100;
    return Math.floor(ddValue);
  }
  updateStockPrice(isUnitPriceUpdate = false) {
    if (this.model?.Stocks?.length > 0) {
      this._commonService.Question(Message.AllowAutoUpdate.replace("#Text", isUnitPriceUpdate ? "Stock unit price" : "stock selling price") as string).then(result => {
        if (result) {
          this.model.Stocks.forEach(x => {
            if (isUnitPriceUpdate) {
              x.UnitPrice = this.model.Price as number
            } else {
              x.SellingPrice = this.model.SellingPrice as number

            }
          });
        }
      })

    }

  }

  onAddStock() {
    this.stockModel = {} as ProductStockModel;
    this.tempStock = undefined;
    this.stockModel.UnitPrice = Number(this.model.Price);
    this.stockModel.SellingPrice = Number(this.model.SellingPrice);

    this.ddlProductSize();
  }

  onEditStock(stockItem: ProductStockModel, idx: number) {
    this.ddlProductSize()
    if (stockItem && stockItem.Id != null) {
      idx = this.model.Stocks.findIndex(x => x.Id === stockItem.Id);
    }
    this.stockModel = Object.assign({}, this.model.Stocks[idx]);
    this.tempStock = Object.assign({}, this.stockModel);
    this.model.Stocks.splice(idx, 1);
  }
  onRemoveStock(idx: number) {
    this.model.Stocks.splice(idx, 1);
  }
  onCancelEdit() {
    if (this.tempStock) {
      this.model.Stocks.push(this.tempStock)
    }
  }

  backToPrevious() {
    history.back();
  }

  onSaveStock() {
    if (this.model.Stocks && this.model.Stocks.some(x => x.SizeId === this.stockModel.SizeId)) {
      this.stockModel.SizeId = undefined;
      this.toast.warning("Size Already exist, Please Select different size...", "Oops");
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
  applySellingPrice() {
    this.stockModel.SellingPrice = Number(this.model.SellingPrice);

  }

  onGetProductSizeLabel(sizeId: any) {
    return this.dropDown?.ddlProductSize?.find(x => x.Value === sizeId)?.Text ?? '';
  }
}
