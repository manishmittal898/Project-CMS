import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
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

  @Input() set Id(value: string) {
    this.id = value;
    this.getDetail();
  }
  @Input() refreshData: boolean = false;
  get DiscountValue() {
    return (Math.round(((this.model?.SellingPrice as number - this.model?.Price) / this.model?.Price) * 100)).toString() + '%';

  }
  constructor(private _activatedRoute: ActivatedRoute, private _productService: ProductService, private readonly toast: ToastrService,) {
    this._activatedRoute.params.subscribe(x => {
      this.id = this._activatedRoute.snapshot.params.id;
      this.getDetail();
    });

  }

  ngOnInit(): void {
    //  this.getDetail();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['refreshData']?.currentValue) {
      this.getDetail();
      this.refreshData = false;
    }

  }
  getDetail(): void {
    if (this.id?.length > 0) {
      this.model = {} as ProductMasterViewModel;
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
  }

  backToPrevious() {
    history.back()
  }

}
