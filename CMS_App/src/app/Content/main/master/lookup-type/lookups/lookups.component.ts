import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { LookupTypeService } from 'src/app/Shared/Services/Master/lookup-type.service';
import { LookupMasterModel, LookupService } from '../../../../../Shared/Services/Master/lookup.service';
import { LookupsAddEditComponent } from './lookups-add-edit/lookups-add-edit.component';

@Component({
  selector: 'app-lookups',
  templateUrl: './lookups.component.html',
  styleUrls: ['./lookups.component.scss']
})
export class LookupsComponent implements OnInit {
  pageName = ''
  model!: LookupMasterModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: string;
  displayedColumns: string[] = ['index', 'Name', 'Value', 'ImagePath', 'SortedOrder', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' },
  { Value: 'SortedOrder', Text: 'Sorted Order' }];
  indexModel = new IndexModel();
  totalRecords = 0;
  iSImageVisible = false;
  isValueVisible = false;
  noRecordData = {
    subject: 'Can you please add your first record.',
    Description: undefined,
    url: undefined,
    urlLable: 'Create'
  };
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _lookupService: LookupService, private readonly _lookupTypeService: LookupTypeService,
    public dialog: MatDialog

  ) {
    _activatedRoute.params.subscribe(x => {
      this.id = this._activatedRoute.snapshot.params.typeId;
      this.pageName = this._activatedRoute.snapshot.params.name;
      this.checkColumn()
      this.getList();
    })
  }
  checkColumn() {
    this._lookupTypeService.GetLookupTypeMaster(this.id).subscribe(x => {
      if (x.IsSuccess) {
        this.displayedColumns = ['index', 'Name',];
        this.iSImageVisible = x.Data?.IsImage as boolean;
        this.isValueVisible = x.Data?.IsValue as boolean;
        if (this.iSImageVisible) {
          this.displayedColumns.push('ImagePath');
        }
        if (this.isValueVisible) {
          this.displayedColumns.push('Value')
        }

        this.displayedColumns.push('SortedOrder', 'IsActive', 'Action');
      }
    })
  }

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.indexModel.AdvanceSearchModel = {};
    this.indexModel.AdvanceSearchModel["typeId"] = this.id;
    this._lookupService.GetList(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as LookupMasterModel[];
        this.dataSource = new MatTableDataSource<LookupMasterModel>(this.model);
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
        let subscription = this._lookupService.ChangeLookupMasterActiveStatus(Id).subscribe(
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
        let subscription = this._lookupService.DeleteLookupMaster(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.model.findIndex(x => x.Id == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<LookupMasterModel>(this.model);
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
    const dialogRef = this.dialog.open(LookupsAddEditComponent, {
      data: { Id: Id, Type: this.id, lookupTypeConfig: { isImage: this.iSImageVisible, isValue: this.isValueVisible }, Heading: `${Id.length > 0 ? 'Update ' : 'Add '} ${this.pageName}` },
      width: '500px',
      panelClass: 'mat-custom-modal'
    });
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        this.getList();
      }
    });
  }
  onClear() {
    this.indexModel.Search = '';
    this.indexModel.Page = 1;
    this.getList();
  }


}


