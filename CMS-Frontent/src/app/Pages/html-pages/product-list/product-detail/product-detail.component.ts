import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/product.service';
declare var $: any;
@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})

export class ProductDetailComponent implements OnInit {
  model = {} as ProductMasterViewModel;
  recordId: number;
  constructor(private readonly _productService: ProductService, private readonly _route: ActivatedRoute) {


    this._route.params.subscribe(x => {
      this.recordId = x.id;
      this.getDetailData();

    });

  }

  ngOnInit(): void {
    //remove Slider after bind data

    //call dynamic data function

    //use inside after dynamic data function
    // setTimeout(() => {
    //   this.AddSlider();
    // }, 50);
  }

  getDetailData() {
    this._productService.GetDetail(this.recordId).subscribe(res => {
      if (res.IsSuccess) {
        this.model = res.Data;
        this.AddSlider();
        setTimeout(() => {
          this.ProductDetailSlider()
        }, 50);

      }
    })

  }


ProductDetailSlider()
{
  $('.product-d-main-slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    dots: false,
    infinite: true,
    speed: 500,
    autoplay: true,
    autoplaySpeed: 4000,
    asNavFor: '.product-d-main-slider-nav'
  });
  $('.product-d-main-slider-nav').slick({
    slidesToShow: 5,
    slidesToScroll: 1,
    arrows: false,
    dots: false,
    infinite: true,
    speed: 500,
    autoplay: true,
    autoplaySpeed: 4000,
    asNavFor: '.product-d-main-slider',
    focusOnSelect: true
  });
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
