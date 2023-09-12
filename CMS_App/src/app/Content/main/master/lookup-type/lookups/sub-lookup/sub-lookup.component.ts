import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { SubLookupMasterViewModel, SubLookupService } from '../../../../../../Shared/Services/Master/sub-lookup.service';
import { SubLookupAddEditComponent } from './sub-lookup-add-edit/sub-lookup-add-edit.component';

@Component({
  selector: 'app-sub-lookup',
  templateUrl: './sub-lookup.component.html',
  styleUrls: ['./sub-lookup.component.scss']
})
export class SubLookupComponent implements OnInit {
  pageName = { Name: "", SubName: "" };
  model!: SubLookupMasterViewModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'ImagePath', 'SortedOrder', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' },
  { Value: 'SortedOrder', Text: 'Sorted Order' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  noRecordData = {
    subject: 'Can you please add your first record.',
    Description: undefined,
    url: undefined,
    urlLable: 'Create'
  };
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _sublookupService: SubLookupService,
    public dialog: MatDialog

  ) {
    _activatedRoute.params.subscribe(x => {
      this.id = this._activatedRoute.snapshot.params.lookupId;
      this.pageName.Name = this._activatedRoute.snapshot.params.name;
      this.pageName.SubName = this._activatedRoute.snapshot.params.subname;

      this.getList();
    })
  }


  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.indexModel.AdvanceSearchModel = {};
    this.indexModel.AdvanceSearchModel["lookupId"] = this.id;
    this._sublookupService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as SubLookupMasterViewModel[];
        this.dataSource = new MatTableDataSource<SubLookupMasterViewModel>(this.model);
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

  OnActiveStatus(Id: string) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._sublookupService.ChangeLookupMasterActiveStatus(Id).subscribe(
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
        let subscription = this._sublookupService.DeleteLookupMaster(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.model.findIndex(x => x.Id == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<SubLookupMasterViewModel>(this.model);
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }

  onAddUpdateLookup(Id: string) {
    const dialogRef = this.dialog.open(SubLookupAddEditComponent, {
      data: { Id: Id, Type: this.id, Heading: `${Id.length > 0 ? 'Update ' : 'Add '} ${this.pageName.SubName} category` },
      width: '500px',
      panelClass: 'mat-custom-modal'
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.getList();
      }
    });
  }
  onBack() {
    window.history.back();
  }
  onClear() {
    this.indexModel.Search = '';
    this.indexModel.Page = 1;
    this.getList();
  }
}

