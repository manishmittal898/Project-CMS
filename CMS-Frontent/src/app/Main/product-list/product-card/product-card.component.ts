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
    return `/store/${this.Product.Category?.replace('/','-').split(' ').join('-')}/${this.Product.Name.replace('/','-').split(' ').join('-')}/${this.Product.Id}`

  }
  updateWishlist() {
    let model = { ProductId: this.Product.Id } as WishListPostModel;
    if (this._auth.IsAuthentication) {
      if (this.Product.IsWhishList) {

        this._wishListService.RemoveProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            this.Product.IsWhishList = !this.Product.IsWhishList;
            this._toasterService.success(x.Message as string, 'Success');
            this.productChange.emit(this.Product);
          } else {
            this._toasterService.error(x.Message as string, 'Faild');

          }
        }, error => {
          this._toasterService.error(error.message as string, 'Failed');
        })

      } else {
        this._wishListService.AddProduct(model).subscribe(x => {
          if (x.IsSuccess) {
            this.Product.IsWhishList = !this.Product.IsWhishList;
            this._toasterService.success(x.Message as string, 'Success');
            this.productChange.emit(this.Product);

          }
          else {
            this._toasterService.error(x.Message as string, 'Faild');

          }
        },
          error => {
            this._toasterService.error(error.message as string, 'Failed');

          })

      }
    } else {

    }

  }

}
