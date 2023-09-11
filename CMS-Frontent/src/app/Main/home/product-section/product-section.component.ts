import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';
import { DropDown_key } from 'src/app/Shared/Constant';
import { ApiResponse, DropDownModel, IDictionary } from 'src/app/Shared/Helper/Common';
import { CommonService } from 'src/app/Shared/Services/Core/common.service';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';
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
  Subscription = {};
  constructor(private _commonService: CommonService, private readonly _productService: ProductService, private _securityService: SecurityService) {
  }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    let that = this;
    if (this._securityService.checkLocalStorage('ddlProductViewSection')) {
      this.dropDown.ddlProductViewSection = JSON.parse(this._securityService.getStorage('ddlProductViewSection'));
      this.loadProductSection();
      if (this._securityService.getStorage("isProductViewSection", true) == undefined) {
        getProductSection();
      }
    } else {
      getProductSection();
    }

    function getProductSection() {

      let serve = that._commonService.GetDropDown([DropDown_key.ddlProductViewSection], true).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          that._securityService.setStorage("isProductViewSection", "true", true);
          const ddls = res?.Data as DropDownModel;
          that.dropDown.ddlProductViewSection = ddls?.ddlProductViewSection;
          that._securityService.setStorage('ddlProductViewSection', JSON.stringify(that.dropDown.ddlProductViewSection));
          that.loadProductSection();
        }
      });
    }
  }

  loadProductSection() {
    let that = this;

    if (this._securityService.checkLocalStorage('loadProductSection')) {
      let data = JSON.parse(this._securityService.getStorage('loadProductSection'));
      this.dropDown.ddlProductViewSection.forEach(itm => {
        if (data[itm.Value]['IsSuccess']) {
          this.model[itm.Value.toString()] = data[itm.Value]['Data']
        }
      })
      //  setTimeout(() => {
      this.AddSlider();
      //  }, 100);
      if (this._securityService.getStorage("isLoadedProductSectionData", true) == undefined) {
        getProductSectionData();
      }
    }
    else {
      getProductSectionData();
    }

    function getProductSectionData() {
      that.Subscription = {};
      that.dropDown.ddlProductViewSection.forEach(x => {
        let indexModel = new ProductFilterModel();
        indexModel.PageSize = 50;
        indexModel.ViewSectionId = [x.Value];
        that.Subscription[x.Value] = that._productService.GetList(indexModel);
      });
      forkJoin(that.Subscription).subscribe((res) => {
        that._securityService.setStorage('loadProductSection', JSON.stringify(res));
        that._securityService.setStorage("isLoadedProductSectionData", "true", true);
        that.dropDown.ddlProductViewSection.forEach(itm => {
          if (res[itm.Value]['IsSuccess']) {
            that.model[itm.Value.toString()] = res[itm.Value]['Data'];
          }

        });
        setTimeout(() => {
          that.AddSlider();
        }, 100);
      });
    }
  };


  private AddSlider() {

    setTimeout(() => {
      $('.product-items-5')?.slick({
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
    }, 100);

  }
}

