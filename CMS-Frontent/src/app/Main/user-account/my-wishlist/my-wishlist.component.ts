import { Component, OnInit } from '@angular/core';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/ProductService/product.service';
import { WishListService } from 'src/app/Shared/Services/ProductService/wish-list.service';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';

@Component({
  selector: 'app-my-wishlist',
  templateUrl: './my-wishlist.component.html',
  styleUrls: ['./my-wishlist.component.css']
})
export class MyWishlistComponent implements OnInit {
  unIndexModel = new ProductFilterModel();
  authIndexModel = new IndexModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  get IsAuthentication() {
    return this._auth.IsAuthentication.value;
  }
  constructor(private readonly _productService: ProductService,
    private readonly _auth: AuthService, private _securityService: SecurityService,
    private readonly _wishListService: WishListService) {

  }

  ngOnInit(): void {
    this.getList();
  }
  getList() {
    if (!this.IsAuthentication) {
      let data = JSON.parse(this._securityService.getStorage('wishlist')) as any[];
      this.unIndexModel.Ids = data
      if (data?.length > 0) {
        this._productService.GetList(this.unIndexModel).subscribe(response => {
          if (response.IsSuccess) {
            this.model = response.Data as ProductMasterViewModel[];
            this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;
          }
        })
      };

    } else {
      this._wishListService.GetList(this.authIndexModel).subscribe(response => {
        if (response.IsSuccess) {
          this.model = response.Data;
          this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

        }
      })
    }
  }
  removeProduct(product: any, index) {
    if (!product.IsWhishList) {
      this.model.splice(this.model.findIndex(x => x.Id == product.Id), 1);
      this.totalRecords--;
    }
  }
}
