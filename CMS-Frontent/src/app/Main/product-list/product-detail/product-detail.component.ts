import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductMasterViewModel, ProductService, ProductStockModel } from 'src/app/Shared/Services/product.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
declare var $: any;
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})

export class ProductDetailComponent implements OnInit {
  model = {} as ProductMasterViewModel;
  recordId: number;
  isLoading = false;
  get totalStock() {
    let stockCount = 0;
    this.model?.Stocks?.forEach(x => {
      stockCount += x.Quantity;
    });
    return stockCount;
  }
  SelectedSizeModel: ProductStockModel;

  shareLink: SafeUrl;
  constructor(private readonly _productService: ProductService, private readonly _route: ActivatedRoute, private readonly _sainitizer: DomSanitizer) {
  }

  ngOnInit(): void {
    this._route.params.subscribe(x => {
      this.recordId = x.id;
      this.getDetailData();
    });

  }

  getDetailData() {
    this.isLoading = true;
    this._productService.GetDetail(this.recordId).subscribe(res => {
      if (res.IsSuccess) {
        this.isLoading = false;
        this.model = res.Data;
        if (this.model?.Stocks?.length > 0) {
          this.SelectedSizeModel = this.model.Stocks[0];
        }
        this.shareLink = this._sainitizer.bypassSecurityTrustResourceUrl(`whatsapp://send?text=${environment.sitePath}/store/${this.model.Name.split(' ').join('-')}/${this.recordId}
         `);

        setTimeout(() => {
          this.ProductDetailSlider();
        }, 100);
      }
    })

  }

  ProductDetailSlider() {

    setTimeout(() => {
      $('.product-d-main-slider')?.slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: true,
        dots: false,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 4000,
        asNavFor: '.product-d-main-slider-nav'
      });


      $('.product-d-main-slider-nav')?.slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: false,
        dots: false,
        infinite: true,
        speed: 500,
        autoplay: true,
        autoplaySpeed: 4000,
        asNavFor: '.product-d-main-slider',
        focusOnSelect: true
      });
    }, 100);

  }


}
