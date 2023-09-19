import { Component, OnInit } from "@angular/core";
import { SafeUrl, DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { SecurityService } from "src/app/Shared/Services/Core/security.service";
import { ProductMasterViewModel, ProductStockModel, ProductService } from "src/app/Shared/Services/ProductService/product.service";
import { WishListService, WishListPostModel } from "src/app/Shared/Services/ProductService/wish-list.service";
import { AuthService } from "src/app/Shared/Services/UserService/auth.service";
import { environment } from "src/environments/environment";
import { CartProductPostModel, CartProductService } from '../../../Shared/Services/ProductService/cart-product.service';

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
  Quantity=1;
  get totalStock() {
    let stockCount = 0;
    this.model?.Stocks?.forEach(x => {
      stockCount += x.Quantity;
    });
    return stockCount;
  }
  SelectedSizeModel: ProductStockModel;
  get DiscountValue() {
    return (Math.round(((this.model?.SellingPrice as number - this.model?.Price) / this.model?.Price) * 100))?.toString() + '%';

  }
  shareLink: SafeUrl;
  constructor(private readonly _productService: ProductService, private readonly _route: ActivatedRoute,
    private readonly _sainitizer: DomSanitizer, private readonly _securityService: SecurityService, private readonly _cartService: CartProductService,
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
        slidesToShow: 6,
        slidesToScroll: 1,
        arrows: true,
        dots: false,
        infinite: false,
        speed: 500,
        autoplay: false,
        autoplaySpeed: 4000,
        asNavFor: '.product-d-main-slider',
        focusOnSelect: true,
        responsive: [
          {
            breakpoint: 1400,
            settings: {
              slidesToShow: 6,
            }
          },
          {
            breakpoint: 1024,
            settings: {
              slidesToShow: 5,
              arrows: false,
            }
          },
        ]
      });
    }, 500);

  }



  updateWishlist() {
    this.loading.WishList = true;
    this._wishListService.SetWishlistProduct(this.model).then(() => {
      setTimeout(() => {
        this.loading.WishList = false;
      }, 1000);
    });
  }

  onAddtoCart() {
    this.loading.AddToCart = true;
    this._cartService.SetCartProduct({ ProductId: this.model.Id, SizeId: this.SelectedSizeModel.SizeId, Quantity: 1 } as CartProductPostModel).then(() => {
      setTimeout(() => {
        this.loading.AddToCart = false;
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
