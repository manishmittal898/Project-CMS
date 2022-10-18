import { SecurityService } from './../../Shared/Services/security.service';
import { Component, OnInit } from '@angular/core';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { ProductCategoryViewModel, ProductService } from 'src/app/Shared/Services/product.service';

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

  ngOnInit(): void {
    this.getCategoryList()
  }

  getCategoryList() {
    this._productService.GetCategoryProduct(this.indexModel).subscribe(response => {

      if (response.IsSuccess) {
        console.log(response.Data)
        this.model = response.Data.map(x => {
          return {
            Id: this._securityService.encrypt(String(x.Id)),
            Name: x.Name,
            ImagePath: x.ImagePath,
          } as any

        });

        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;

        setTimeout(() => {
          this._securityService?.setStorage('home-page-product', JSON.stringify(this.model))

        }, 10);

      }
    })
  }

}
