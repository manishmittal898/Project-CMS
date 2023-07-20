import { SecurityService } from './../../Shared/Services/security.service';
import { Component, OnInit } from '@angular/core';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { ProductCategoryViewModel, ProductService } from 'src/app/Shared/Services/product.service';
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
  constructor(private readonly _productService: ProductService, private readonly _securityService: SecurityService) {
    if (this._securityService.getStorage('home-page-product')) {
      this.model = JSON.parse(this._securityService.getStorage('home-page-product'));
    }
  }
  isLoading = false;
  ngOnInit(): void {
    this.getCategoryList()
    this.AddSlider()
  }

  getCategoryList() {
    this._productService.GetCategoryProduct(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data.map(x => {
          return {
            Id: this._securityService.encrypt(String(x.Id)),
            Name: x.Name,
            ImagePath: x.ImagePath,
          } as any

        });
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

        this.AddSlider();
        setTimeout(() => {
          this._securityService?.setStorage('home-page-product', JSON.stringify(this.model))
        }, 10);

      }
    })
  }

  AddSlider() {
    this.isLoading = false;

    setTimeout(() => {
      $('.slider-items-5')?.slick({
        dots: true,
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
            slidesToShow: 1,
            slidesToScroll: 1
          }
        }
        ]
      });

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

    }, 50);

  }
}
