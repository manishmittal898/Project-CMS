import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { DropDown_key } from 'src/app/Shared/Constant';
import { DropDownModel, IDictionary } from 'src/app/Shared/Helper/Common';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { ProductFilterModel, ProductMasterViewModel, ProductService } from 'src/app/Shared/Services/ProductService/product.service';

declare var $: any;
@Component({
  selector: 'app-product-section',
  templateUrl: './product-section.component.html',
  styleUrls: ['./product-section.component.css']
})
export class ProductSectionComponent implements OnInit {
  dropDown = new DropDownModel();
  model = {} as IDictionary<ProductMasterViewModel[]>;

  constructor(private _commonService: CommonService, private readonly _productService: ProductService) {

  }

  ngOnInit(): void {
    this.GetDropDown();

  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlProductViewSection], true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        debugger
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlProductViewSection = ddls?.ddlProductViewSection;
        this.loadProductSection();
      }
    });
  }
  loadProductSection() {
    let indexModel = new ProductFilterModel();
    indexModel.ViewSectionId = this.dropDown.ddlProductViewSection.map(x => Number(x.Value));

    this._productService.GetList(indexModel).subscribe(response => {
      if (response.IsSuccess) {
        let data = response.Data;

        this.dropDown.ddlProductViewSection.forEach(X => {
          this.model[X.Value.toString()] = data.filter(x => x.ViewSectionId == X.Value)
        })
        //setTimeout(() => {
          this.AddSlider();
       // }, 100);

      }
    })

  };


  private AddSlider() {

    setTimeout(() => {
      $('.product-items-5')?.slick({
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
    }, 100);

  }
}

