import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GeneralEntryService, GeneralEntryCategoryViewModel } from 'src/app/Shared/Services/Master/general-entry.service';

@Component({
  selector: 'app-general-entry-category-master-detail',
  templateUrl: './general-entry-category-master-detail.component.html',
  styleUrls: ['./general-entry-category-master-detail.component.scss']
})
export class GeneralEntryCategoryMasterDetailComponent implements OnInit {
  recordId = '';
  model = {} as GeneralEntryCategoryViewModel;
  constructor(private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService, private readonly _generalEntryService: GeneralEntryService) {

  }
  ngOnInit(): void {
    this._activatedRoute.params.subscribe(x => {
      this.recordId = this._activatedRoute.snapshot.params.id ? this._activatedRoute.snapshot.params.id : 0;
      if (this.recordId.length) {
        this.onGetDetail();
      }
    });
  }

  onGetDetail() {
    this._generalEntryService.GetGeneralEntryCategory(this.recordId).subscribe(response => {
      if (response.IsSuccess) {
        this.model = response.Data as GeneralEntryCategoryViewModel;

      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
}
