import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GeneralEntryService ,GeneralEntryViewModel} from '../../../../Shared/Services/Master/general-entry.service';

@Component({
  selector: 'app-general-entry-master',
  templateUrl: './general-entry-master.component.html',
  styleUrls: ['./general-entry-master.component.scss']
})
export class GeneralEntryMasterComponent implements OnInit {


  model!: GeneralEntryViewModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['index', 'Title', 'ImagePath', 'Category', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Title', Text: 'Title' },
  { Value: 'Category', Text: 'Category' }, ];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  noRecordData = {
    subject: 'Can you please add your first general Entry.',
    Description: "You haven't added general Entry yet. please add your new general Entrys",
    url: '../add',
    urlLable: 'Add New general Entry'
  };
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _generalEntryService: GeneralEntryService) {
    _activatedRoute.params.subscribe(x => {
      this.getList();
    })
  }


  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this._generalEntryService.GetListGeneralEntry(this.indexModel).subscribe(response => {
      if (response.IsSuccess) {
        debugger
        this.model = response.Data as GeneralEntryViewModel[];
        this.dataSource = new MatTableDataSource<GeneralEntryViewModel>(this.model);
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

  OnActiveStatus(Id: number) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._generalEntryService.ChangeGeneralEntryActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Remove');
              const idx = this.model.findIndex(x => x.Id == Id);
              this.model[idx].IsActive = !this.model[idx].IsActive;
              this.dataSource = new MatTableDataSource<GeneralEntryViewModel>(this.model);
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
        let subscription = this._generalEntryService.DeleteGeneralEntry(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              debugger
              const idx = this.model.findIndex(x => x.Id == id);
              this.model.splice(idx, 1);
              this.totalRecords--;
              this.dataSource = new MatTableDataSource<GeneralEntryViewModel>(this.model);
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
