import { Component, OnInit, OnChanges, Input, SimpleChanges } from "@angular/core";
import { SecurityService } from "src/app/Shared/Services/Core/security.service";
import { ProductFilterModel, ProductMasterViewModel, ProductService } from "src/app/Shared/Services/ProductService/product.service";

declare var $: any;
@Component({
  selector: 'app-category-product-list',
  templateUrl: './category-product-list.component.html',
  styleUrls: ['./category-product-list.component.css']
})
export class CategoryProductListComponent implements OnInit, OnChanges {
  @Input() CategoryId: string;
  @Input() SubCategoryId: string;
  @Input() ExcludeId: string = '';

  indexModel = new ProductFilterModel();
  model: ProductMasterViewModel[] = [];
  productModel: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  isLoading = false;
  // get productModel() {
  //   this.isLoading = true;
  //   this.AddSlider();
  //   return this.model.filter(x => Number(x.Id) !== Number(this.ExcludeId));

  // }
  constructor(private readonly _productService: ProductService, private readonly _securityService: SecurityService) {
    this.indexModel.CategoryId = [this.CategoryId];
  }

  ngOnChanges(changes: SimpleChanges): void {

    if (changes && changes?.CategoryId?.currentValue != changes?.CategoryId?.previousValue) {
      this.getList();
    }

    if (changes && changes?.ExcludeId?.currentValue != changes?.ExcludeId?.previousValue) {
      this.isLoading = true;
      setTimeout(() => {
        this.bindRelatedList();

      }, 100);
    }
  }

  ngOnInit(): void {
    this.getList();
  }
  getList() {
    this.isLoading = true;

    if (this.CategoryId.length > 0) {
      this.indexModel.CategoryId = [this.CategoryId];
      this._productService.GetList(this.indexModel).subscribe(response => {
        if (response.IsSuccess) {
          this.model = response.Data;
          this.model = this.model; //.map(x => { return { ...x, Id: this._securityService.encrypt(String(x.Id)) as any } });
          this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord - (this.ExcludeId.length > 0 ? 1 : 0) : 0) as number;
          this.bindRelatedList();

        }
      });
    }

  }
  bindRelatedList() {
    this.productModel = this.model.filter(x => x.Id !== this.ExcludeId);
    this.isLoading = false;
    this.AddSlider();

  }
  AddSlider() {
      $('.slider-items-5')?.slick({
        dots: false,
        infinite: true,
        speed: 1000,
        slidesToShow: 5,
        slidesToScroll: 5,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [{
          breakpoint: 1400,
          settings: {
            slidesToShow: 4,
            slidesToScroll: 4,
            infinite: true,
            dots: true
          }
        },
        {
          breakpoint: 1200,
          settings: {
            slidesToShow: 3,
            slidesToScroll: 3
          }
        },
        {
          breakpoint: 767,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2
          }
        },
        {
          breakpoint: 595,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2
          }
        }
        ]
      });

  }

}
