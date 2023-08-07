import { WishListService } from './../../../Shared/Services/ProductService/wish-list.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductMasterViewModel } from 'src/app/Shared/Services/ProductService/product.service';
import { WishListPostModel } from '../../../Shared/Services/ProductService/wish-list.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

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
      this.Product.IsWhishList = this._wishListService.wishListItem.find(x => x.Id == this.Product.Id).IsWhishList ?? this.Product.IsWhishList;
    }
  }
  getUrl() {
    return `/store/${this.Product.Category?.replace('/', '-').split(' ').join('-')}/${this.Product.Name.replace('/', '-').split(' ').join('-')}/${this.Product.Id}`

  }
  updateWishlist() {

    this.loading.WishList = true;
    this._wishListService.SetWishlistProduct(this.Product).then(() => {
      setTimeout(() => {
        this.loading.WishList = false;
        this.productChange.emit(this.Product);
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
