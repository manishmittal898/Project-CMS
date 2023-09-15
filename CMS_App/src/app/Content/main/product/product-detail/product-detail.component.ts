import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  model = {} as ProductMasterViewModel;
  id: string = "";
 get DiscountValue() {
    return (Math.round(((this.model?.SellingPrice as number - this.model?.Price )/this.model?.Price)*100)).toString()+'%';

   }
  constructor(private _activatedRoute: ActivatedRoute, private _productService: ProductService, private readonly toast: ToastrService,) {
    this._activatedRoute.params.subscribe(x => {
      this.id = this._activatedRoute.snapshot.params.id;

    });

  }

  ngOnInit(): void {
    this.getDetail();
  }
  getDetail(): void {
    this._productService.GetProductMaster(this.id).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as ProductMasterViewModel;

      } else {

        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
  backToPrevious(){
    history.back()
  }

}
