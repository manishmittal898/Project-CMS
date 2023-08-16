import { Component, OnInit } from "@angular/core";
import { SafeUrl, DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { SecurityService } from "src/app/Shared/Services/Core/security.service";
import { ProductMasterViewModel, ProductStockModel, ProductService } from "src/app/Shared/Services/ProductService/product.service";
import { WishListService, WishListPostModel } from "src/app/Shared/Services/ProductService/wish-list.service";
import { AuthService } from "src/app/Shared/Services/UserService/auth.service";
import { environment } from "src/environments/environment";

declare var $: any;
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})

export class ProductDetailComponent implements OnInit {
  model = {} as ProductMasterViewModel;
  recordId: string;
  isLoading = false;
  loading = new Loading();
  get totalStock() {
    let stockCount = 0;
    this.model?.Stocks?.forEach(x => {
      stockCount += x.Quantity;
    });
    return stockCount;
  }
  SelectedSizeModel: ProductStockModel;

  shareLink: SafeUrl;
  constructor(private readonly _productService: ProductService, private readonly _route: ActivatedRoute,
    private readonly _sainitizer: DomSanitizer, private readonly _securityService: SecurityService,
    private readonly _wishListService: WishListService, private _toasterService: ToastrService, private _auth: AuthService) {
  }

  ngOnInit(): void {
    this._route.params.subscribe(x => {
      this.recordId = x.id as string;
      this.getDetailData();
    });

  }

  getDetailData() {
    this.isLoading = true;
    this._productService.GetDetail(this.recordId).subscribe(res => {
      if (res.IsSuccess) {
        debugger
        this.isLoading = false;
        this.model = res.Data;

        if (this.model?.Stocks?.length > 0) {

          this.SelectedSizeModel = this.model.Stocks[0];
        }
        this.shareLink = this._sainitizer.bypassSecurityTrustResourceUrl(`whatsapp://send?text=${environment.sitePath}/collections/${this.model.Category.split(' ').join('-')}/${this.model.Name.split(' ').join('-')}/${this.recordId}
         `);
        if (!this._auth.IsAuthentication.value && this._wishListService?.wishListItem?.length > 0) {
          this.model.IsWhishList = this._wishListService?.wishListItem?.some(x => x == this.model.Id);
        }

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
        infinite: false,
        speed: 500,
        autoplay: false,
        autoplaySpeed: 4000,
        asNavFor: '.product-d-main-slider-nav'
      });


      $('.product-d-main-slider-nav')?.slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        arrows: false,
        dots: false,
        infinite: false,
        speed: 500,
        autoplay: false,
        autoplaySpeed: 4000,
        asNavFor: '.product-d-main-slider',
        focusOnSelect: true
      });
    }, 500);

  }

  getThumbnailPath(filePath) {
    if (filePath) {
      let image = filePath?.split('/');
      image.splice(image.length - 1, 0, 'Thumbnail');
      return image.join('/');
    } else {
      return null;
    }

  }

  updateWishlist() {
    this.loading.WishList = true;
    this._wishListService.SetWishlistProduct(this.model).then(() => {
      setTimeout(() => {
        this.loading.WishList = false;
      }, 1000);
    });
  }


}
export class Loading {
  WishList: boolean = false;
  AddToCart: boolean = false;

  reset() {
    this.AddToCart = false;
    this.WishList = false;
  }

}
