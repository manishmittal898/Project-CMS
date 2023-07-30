import { WishListService } from './../../../Shared/Services/ProductService/wish-list.service';
import { Component, Input, OnInit } from '@angular/core';
import { ProductMasterViewModel } from 'src/app/Shared/Services/ProductService/product.service';
import { WishListPostModel } from '../../../Shared/Services/ProductService/wish-list.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {
  @Input() Product = {} as ProductMasterViewModel;
  constructor(private readonly _wishListService: WishListService, private _toasterService: ToastrService,) { }

  ngOnInit(): void {
  }
  getUrl() {
    return `/store/${this.Product.Category?.split(' ').join('-')}/${this.Product.Name.split(' ').join('-')}/${this.Product.Id}`

  }
  updateWishlist() {
    let model = { ProductId: this.Product.Id } as WishListPostModel;
    if (this.Product.IsWhishList) {

      this._wishListService.RemoveProduct(model).subscribe(x => {
        if (x.IsSuccess) {
          this.Product.IsWhishList = !this.Product.IsWhishList;
          this._toasterService.success(x.Message as string, 'Success');

        } else {
          this._toasterService.error(x.Message as string, 'Faild');

        }
      }, error => {
        this._toasterService.error(error.message as string, 'Failed');

      })
      //remove wishlist API
    } else {
      this._wishListService.AddProduct(model).subscribe(x => {
        if (x.IsSuccess) {
          this.Product.IsWhishList = !this.Product.IsWhishList;
          this._toasterService.success(x.Message as string, 'Success');

        }
        else {
          this._toasterService.error(x.Message as string, 'Faild');

        }
      },
        error => {
          this._toasterService.error(error.message as string, 'Failed');

        })

    }
  }
}
