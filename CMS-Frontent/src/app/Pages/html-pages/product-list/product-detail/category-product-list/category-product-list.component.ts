import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';
declare var $: any;
@Component({
  selector: 'app-category-product-list',
  templateUrl: './category-product-list.component.html',
  styleUrls: ['./category-product-list.component.css']
})
export class CategoryProductListComponent implements OnInit, OnChanges {
  @Input() CategoryId: number;
  @Input() SubCategoryId: number;
  @Input() ExcludeId: number = 0;

  indexModel = new ProductFilterModel();
  model: ProductMasterViewModel[] = [];
  totalRecords: number = 0;
  get productModel() {
    return this.model.filter(x => x.Id != this.ExcludeId);
  }
  constructor(private readonly _productService: ProductService) {

    this.indexModel.CategoryId = [Number(this.CategoryId)];

  }
  ngOnChanges(changes: SimpleChanges): void {

    if (changes && changes?.CategoryId?.currentValue != changes?.CategoryId?.previousValue) {
      this.getList();
    }

    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.

  }

  ngOnInit(): void {
    this.getList();
  }
  getList() {
    if (this.CategoryId > 0) {
      this.indexModel.CategoryId = [Number(this.CategoryId)];
      this._productService.GetList(this.indexModel).subscribe(response => {
        debugger
        if (response.IsSuccess) {
          this.model = response.Data;
          this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord - (this.ExcludeId > 0 ? 1 : 0) : 0) as number;
             setTimeout(() => {
      this.AddSlider();
    }, 50);
        }
      });
    }

  }

  AddSlider() {
    $('.slider-items-4').slick({
      dots: true,
      infinite: true,
      speed: 1000,
      slidesToShow: 4,
      slidesToScroll: 4,
      autoplay: true,
      autoplaySpeed: 2000,
      responsive: [{
        breakpoint: 1600,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3,
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
        breakpoint: 600,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
      ]
    });
  }

}
