import { ProductFilterModel, ProductMasterViewModel, ProductService } from './product.service';
import { AuthService } from './../UserService/auth.service';
import { ToastrService } from 'ngx-toastr';
import { BaseAPIService } from './../Core/base-api.service';
import { Injectable } from '@angular/core';
import { Observable, forkJoin } from 'rxjs';
import { IndexModel, ApiResponse } from '../../Helper/Common';
import { SecurityService } from '../Core/security.service';

@Injectable({
  providedIn: "root"
})
export class CartProductService {

  private cartProductItem: CartProductPostModel[] = [];
  CartProductModel?: CartProductViewModel[] = [];
  constructor(private readonly _baseService: BaseAPIService, private _toasterService: ToastrService,
    private _auth: AuthService, private _securityService: SecurityService, private readonly _productService: ProductService) {
    this.cartProductItem = this._securityService.checkStorage('cart-product') ? JSON.parse(this._securityService.getStorage('cart-product') as string) as any[] : [];
    this.GetCartList();

  }

  private GetList(model: IndexModel): Observable<ApiResponse<CartProductViewModel[]>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Api}`;
    return this._baseService.post(url, model);
  }

  private AddProduct(model: CartProductPostModel): Observable<ApiResponse<CartProductViewModel>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Add_Api}`;
    return this._baseService.post(url, model);
  }

  private RemoveProduct(model: CartProductPostModel): Observable<ApiResponse<CartProductViewModel>> {
    let url = `${this._baseService.API_Url.UserCartProduct_Remove_Api}`;
    return this._baseService.post(url, model);
  }

  public async SetCartProduct(product: CartProductPostModel) {
    //  let model = { ProductId: product.Id } as CartProductPostModel;
    var indx = this.cartProductItem.findIndex(x => x.ProductId == product.ProductId && x.SizeId == product.SizeId);
    if (indx >= 0) {
      let productItem = Object.assign({}, this.cartProductItem[indx]);
      productItem.Quantity += product.Quantity;
      // product = productItem;
    }
    if (this._auth.IsAuthentication.value) {
      this.AddProduct(product).subscribe(x => {
        if (x.IsSuccess) {
          if (indx == -1) {
            this.cartProductItem.push(product);
          }
          this._toasterService.success(x.Message as string, 'Success');
        }
        else {
          this._toasterService.error(x.Message as string, 'Faild');
        }
        let data = JSON.stringify(this.cartProductItem);
        this._securityService.setStorage('cart-product', data);
        this.GetCartList();
        return x;
      },
        error => {
          this._toasterService.error(error.message as string, 'Failed');
        })

    } else {
      if (indx == -1) {
        this.cartProductItem.push(product);
      } else {
        this.cartProductItem[indx].Quantity += product.Quantity;
      }
      this._toasterService.success("Added Successfully" as string, 'Success');
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
      this.GetCartList();
    }
  }
  public async GetCartList() {
    if (!this._auth.IsAuthentication.value) {
      let data = this.cartProductItem.map(x => x.ProductId);
      let indexModel = new ProductFilterModel();
      indexModel.Ids = data;
      indexModel.PageSize = 101;
      if (data?.length > 0) {
        let calls: any = {};
        this.cartProductItem.forEach(x => {
          calls[x.ProductId] = this._productService.GetDetail(x.ProductId);
        })
        forkJoin(calls).subscribe(res => {
          this.CartProductModel = [];
          this.cartProductItem.forEach(element => {
            let response = res[element.ProductId] as ApiResponse<ProductMasterViewModel>
            if (response.IsSuccess) {
              let itm = {} as CartProductViewModel;
              itm.Product = response.Data;
              itm.Id = '';
              itm.Quantity = element.Quantity;
              itm.SizeId = element.SizeId;
              itm.ProductId = element.ProductId;
              this.CartProductModel.push(itm);
            }
          });
        })

      } else {
        this.CartProductModel = [];
      }

    } else {
      let indexModel = new IndexModel();
      indexModel.PageSize = 101;
      this.GetList(indexModel).subscribe(response => {
        if (response.IsSuccess) {
          this.CartProductModel = response.Data;

        }
      })
    }
  }

  public async syncCartProduct() {
    debugger
    let sub = [] as any[];
    this.cartProductItem.forEach(x => {
      sub.push(this.AddProduct(x))
    })
    forkJoin(sub).subscribe(res => {
      this.cartProductItem = [];
      this._securityService.removeStorage('cart-product');
      this.GetCartList();
    })
  }

  public async deleteProduct(productId, sizeId) {
    if (this._auth.IsAuthentication.value) {
      let indx = this.CartProductModel.findIndex(x => x.ProductId == productId && x.SizeId == sizeId)
      let model = {} as CartProductPostModel;
      model.ProductId = this.CartProductModel[indx].ProductId;
      model.SizeId = this.CartProductModel[indx].SizeId;
      model.Quantity = this.CartProductModel[indx].Quantity;
      this.RemoveProduct(model).toPromise().then(x => {
        if (x.IsSuccess) {
          let idx = this.cartProductItem.findIndex(x => x.ProductId == productId);
          this.cartProductItem.splice(idx, 1);
          this.CartProductModel.splice(indx, 1);
          let data = JSON.stringify(this.cartProductItem);
          this._securityService.setStorage('cart-product', data);
          // this.GetCartList();
          //let idx = this.CartProductModel.findIndex(s => s.ProductId == productId && s.SizeId == sizeId);
        }
        return x.IsSuccess;
      })


    } else {
      let indx = this.cartProductItem.findIndex(x => x.ProductId == productId && x.SizeId == sizeId)
      this.cartProductItem.splice(indx, 1);
      let data = JSON.stringify(this.cartProductItem);
      this._securityService.setStorage('cart-product', data);
      let idx = this.CartProductModel.findIndex(s => s.ProductId == productId && s.SizeId == sizeId);
      this.CartProductModel.splice(idx, 1);
      // this.GetCartList();
      return true;
    }

  }

}

export interface CartProductViewModel {
  Id: string;
  ProductId: string;
  SizeId: string;
  Size: string;
  Quantity: number;
  AddedOn: string;
  Product: ProductMasterViewModel;
}

export interface CartProductPostModel {
  ProductId: string;
  SizeId: string;
  Quantity: number;


}
