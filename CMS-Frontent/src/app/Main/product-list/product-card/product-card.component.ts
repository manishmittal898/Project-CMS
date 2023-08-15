import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { ProductMasterViewModel } from "src/app/Shared/Services/ProductService/product.service";
import { WishListService } from "src/app/Shared/Services/ProductService/wish-list.service";

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() Product = {} as ProductMasterViewModel;
  @Output() productChange = new EventEmitter<ProductMasterViewModel>();
  loading = new Loading();
  constructor(private readonly _wishListService: WishListService) { }

  ngOnInit(): void {
    if (this._wishListService.wishListItem.length > 0) {
      this.Product.IsWhishList = this._wishListService.wishListItem?.findIndex(x => x == this.Product.Id) >= 0 ? true : false ?? this.Product.IsWhishList;
    }
  }
  getUrl() {
    return `/collections/${this.Product.Category?.replace('/', '-').split(' ').join('-')}/${this.Product.Name.replace('/', '-').split(' ').join('-')}/${this.Product.Id}`
  }

  updateWishlist() {
    this.loading.WishList = true;
    this._wishListService.SetWishlistProduct(this.Product).then(() => {
      setTimeout(() => {
        this.productChange.emit(this.Product);
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
