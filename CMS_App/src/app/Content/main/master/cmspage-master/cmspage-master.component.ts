
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { IndexModel } from "src/app/Shared/Helper/common-model";
import { Message } from "src/app/Shared/Helper/constants";
import { CMSPageListViewModel, CMSPageMasterService } from "src/app/Shared/Services/cmspage-master.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { LookupsAddEditComponent } from "../lookup-type/lookups/lookups-add-edit/lookups-add-edit.component";
import { LookupTypeEnum } from '../../../../Shared/Enum/fixed-value';


@Component({
  selector: 'app-cmspage-master',
  templateUrl: './cmspage-master.component.html',
  styleUrls: ['./cmspage-master.component.scss']
})
export class CMSPageMasterComponent implements OnInit {
  pageName = 'CMS Page';
  model!: CMSPageListViewModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'SortedOrder', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' },
  { Value: 'SortedOrder', Text: 'Sorted Order' }];
  indexModel = new IndexModel();
  totalRecords = 0;
  noRecordData = {
    subject: 'Can you please add your first record.',
    Description: undefined,
    url: undefined,
    urlLabel: 'Create'
  };
  selectedRecord = {} as any;
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _cmsPageService: CMSPageMasterService,
    public dialog: MatDialog
  ) { }


  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.indexModel.AdvanceSearchModel = {};
    this._cmsPageService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as CMSPageListViewModel[];
        this.dataSource = new MatTableDataSource<CMSPageListViewModel>(this.model);
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
        let subscription = this._cmsPageService.ChangeCMSPageActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Remove');
              this.getList();
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
        let subscription = this._cmsPageService.DeleteCMSPage(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.model.findIndex(x => x.PageId == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<CMSPageListViewModel>(this.model);
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }

  onAddUpdateLookup(Id: string = '') {
    const dialogRef = this.dialog.open(LookupsAddEditComponent, {
      data: { Id: Id, Type: LookupTypeEnum.CMS_Page, lookupTypeConfig: { isImage: false, isValue: false }, Heading: `${Id.length > 0 ? 'Update ' : 'Add '} ${this.pageName}` },
      width: '500px',
      panelClass: 'mat-custom-modal'
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.getList();
      }
    });
  }

  setSelectedRecord(item: CMSPageListViewModel) {
    debugger
    this.selectedRecord = {
      Id: item.PageId,
      PageName: item?.Name?.split(' ')?.join('_') ?? ''
    }
  }
  onClear() {
    this.indexModel.Search = '';
    this.indexModel.Page = 1;
    this.getList();
  }


  isDataRefresh: boolean = false;
  reloadData(status: boolean) {
    this.isDataRefresh = status;
    if (status) {
      this.getList();
      setTimeout(() => {
        this.isDataRefresh = !status;
      }, 15);

    }
  }

}
