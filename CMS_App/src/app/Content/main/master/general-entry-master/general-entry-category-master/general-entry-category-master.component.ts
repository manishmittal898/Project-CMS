import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GeneralEntryCategoryViewModel, GeneralEntryService } from '../../../../../Shared/Services/Master/general-entry.service';

@Component({
  selector: 'app-general-entry-category-master',
  templateUrl: './general-entry-category-master.component.html',
  styleUrls: ['./general-entry-category-master.component.scss']
})
export class GeneralEntryCategoryMasterComponent implements OnInit {

  pageName = 'General Entry Category'
  model!: GeneralEntryCategoryViewModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  displayedColumns: string[] = ['index', 'Name', 'ContentTypeText', 'ImagePath', 'SortedOrder', 'Flags', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' },
  { Value: 'ContentTypeText', Text: 'Content Type' },
  { Value: 'SortedOrder', Text: 'Sorted Order' }];
  indexModel = new IndexModel();
  totalRecords = 0;

  noRecordData = {
    subject: 'Could you please add your first record.',
    Description: 'No Record Found, please add new record!',
    url: './add',
    urlLable: 'Create'
  };
  constructor(private _router: Router, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _generalEntryService: GeneralEntryService,
    public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.indexModel.AdvanceSearchModel = {};
    this._generalEntryService.GetListEntryCategory(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as GeneralEntryCategoryViewModel[];
        this.dataSource = new MatTableDataSource<GeneralEntryCategoryViewModel>(this.model);
        this.totalRecords = (Number(response.TotalRecord) > 0 ? response.TotalRecord : 0) as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        // Toast message if  return false ;
        this.toast.error(response.Message?.toString(), 'Error data');
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

  OnActiveStatus(Id: number) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._generalEntryService.ChangeGeneralEntryCategoryActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Update');
              let idx = this.model.findIndex(x => x.Id == Id);
              this.model[idx].IsActive = !this.model[idx].IsActive;
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

  onFlagStatus(Id: number, columnName: string) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._generalEntryService.ChangeGeneralEntryCategoryFlagStatus(Id, columnName).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              debugger
              this.toast.success(data.Message as string, 'Update');
              //this.getList();
              let idx = this.model.findIndex(x => x.Id == Id);
              switch (columnName) {
                case 'IsShowInMain':
                  this.model[idx].IsShowInMain = !this.model[idx].IsShowInMain;
                  break;
                case 'IsShowDataInMain':
                  this.model[idx].IsShowDataInMain = !this.model[idx].IsShowDataInMain;

                  break;
                case 'IsSingleEntry':
                  this.model[idx].IsSingleEntry = !this.model[idx].IsSingleEntry;
                  break;

                  case 'IsShowThumbnail':
                    this.model[idx].IsShowThumbnail = !this.model[idx].IsShowThumbnail;
                    break;


                default:
                  break;
              }
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


  updateDeleteStatus(id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
      if (result) {
        let subscription = this._generalEntryService.DeleteGeneralEntryCategory(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.model.findIndex(x => x.Id == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<GeneralEntryCategoryViewModel>(this.model);
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


}
