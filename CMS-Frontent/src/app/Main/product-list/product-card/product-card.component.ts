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
  constructor(private readonly _wishListService: WishListService, private _toasterService: ToastrService, private _auth: AuthService) { }

  ngOnInit(): void {
  }
  getUrl() {
    return `/store/${this.Product.Category?.replace('/', '-').split(' ').join('-')}/${this.Product.Name.replace('/', '-').split(' ').join('-')}/${this.Product.Id}`

  }
  updateWishlist() {

    this._wishListService.SetWishlistProduct(this.Product);

  }

}
