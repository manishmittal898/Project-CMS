import { Component, OnInit } from "@angular/core";
import { IndexModel } from "src/app/Shared/Helper/Common";
import { SecurityService } from "src/app/Shared/Services/Core/security.service";
import { ProductCategoryViewModel, ProductService } from "src/app/Shared/Services/ProductService/product.service";
import { GeneralEntryService, GeneralEntryFilterModel, GeneralEntryEnumValue, GeneralEntryViewModel } from '../../Shared/Services/GeneralEntryService/general-entry.service';

declare var $: any;
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  indexModel = new IndexModel();
  model: ProductCategoryViewModel[] = [];
  totalRecords: number = 0;
  BannerImages: GeneralEntryViewModel[] = [];
  constructor(private readonly _productService: ProductService, private readonly _generalEntryService: GeneralEntryService,
    private readonly _securityService: SecurityService) {
    if (this._securityService.getStorage('home-page-product')) {
      this.model = JSON.parse(this._securityService.getStorage('home-page-product'));
    }
  }
  isLoading = false;
  ngOnInit(): void {
    this.getCategoryList()
    this.getBannerImages().then(res => {

    });

  }

  getCategoryList() {
    this._productService.GetCategoryProduct(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data.map(x => {
          return {
            Id: x.Id,
            Name: x.Name,
            ImagePath: x.ImagePath,
          } as any

        });
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

        this.AddSlider();
        setTimeout(() => {
          this._securityService?.setStorage('home-page-product', JSON.stringify(this.model))
        }, 500);

      }
    })
  }
  openURL(item) {
    window.open(item);
  }
  async getBannerImages() {

    let model = new GeneralEntryFilterModel();
    model.EnumValue = GeneralEntryEnumValue.Banner_Image;
    model.PageSize = 10;
    this._generalEntryService.GetList(model).subscribe(res => {
      if (res.IsSuccess) {

        this.BannerImages = res.Data;
      }
    })
  }


  AddSlider() {
    this.isLoading = false;
    $('.slider-items-6')?.slick({
      dots: true,
      infinite: true,
      speed: 1000,
      slidesToShow: 6,
      slidesToScroll: 6,
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
        breakpoint: 1400,
        settings: {
          slidesToShow: 4,
          slidesToScroll: 4
        }
      },
      {
        breakpoint: 900,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3
        }
      },
      {
        breakpoint: 767,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 3
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
