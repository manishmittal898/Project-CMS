
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ProductMasterViewModel, ProductService } from '../../../Shared/Services/product.service';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  model!: ProductMasterViewModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  selectedRecord = "";
  ViewMode = "Detail"
  displayedColumns: string[] = ['index', 'SKU', 'Name', 'ImagePath', 'Category', 'SubCategory', 'Price', 'IsActive', 'Action'];
  ViewdisplayedColumns = [
    { Value: 'Name', Text: 'Name' },
    { Value: 'Category', Text: 'Category' },
    { Value: 'SubCategory', Text: 'Sub Category' },
    { Value: 'CaptionTag', Text: 'Caption Tag' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  noRecordData = {
    subject: 'Can you please add your first product.',
    Description: "You haven't added product yet. please add your new products",
    url: '../add',
    urlLabel: 'Add New Product'
  };
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _productService: ProductService) {
    _activatedRoute.params.subscribe(x => {
      this.getList();
    })
  }


  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this._productService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as ProductMasterViewModel[];
        this.dataSource = new MatTableDataSource<ProductMasterViewModel>(this.model);
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        // Toast message if  return false ;
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }

  sortData(event: any): void {
    this.indexModel.OrderBy = event.active;
    this.indexModel.OrderByAsc = event.direction == "asc" ? true : false;
    this.indexModel.IsPostBack = true;
    this.getList();
  }

  onSearch() {
    this.indexModel.Page = 1;
    this.getList();
  }

  onPaginateChange(event: any) {
    this.indexModel.Page = event.pageIndex + 1;
    this.indexModel.PageSize = event.pageSize;
    this.indexModel.IsPostBack = true;
    this.getList();
  }

  OnActiveStatus(Id: string) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._productService.ChangeProductMasterActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Remove');
              const idx = this.model.findIndex(x => x.Id == Id);
              this.model[idx].IsActive = !this.model[idx].IsActive;
              this.dataSource = new MatTableDataSource<ProductMasterViewModel>(this.model);
            } else {
              this.toast.warning(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.Message as string, 'Error');
          }
        );
      }
    });

  }

  updateDeleteStatus(id: string) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
      if (result) {
        let subscription = this._productService.DeleteProductMaster(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.model.findIndex(x => x.Id == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<ProductMasterViewModel>(this.model);
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }
  onClear() {
    this.indexModel.Search = '';
    this.indexModel.Page = 1;
    this.getList();
  }
  changeViewMode() {
    this.ViewMode = (this.ViewMode == 'Edit' ? 'Detail' : 'Edit');
  }
  isDataRefresh: boolean = false;
  reloadData(value: { status: boolean, recordId: string }) {
    this.isDataRefresh = value.status;
    if (value.status) {
      this.getList();
      setTimeout(() => {
        this.selectedRecord = value.recordId
        this.isDataRefresh = !value.status;
        this.ViewMode = 'Detail';
      }, 15);

    }
  }
}
