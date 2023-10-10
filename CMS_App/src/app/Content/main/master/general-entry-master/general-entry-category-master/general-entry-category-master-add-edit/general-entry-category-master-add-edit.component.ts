import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GeneralEntryCategoryPostModel, GeneralEntryCategoryViewModel, GeneralEntryService } from 'src/app/Shared/Services/Master/general-entry.service';

@Component({
  selector: 'app-general-entry-category-master-add-edit',
  templateUrl: './general-entry-category-master-add-edit.component.html',
  styleUrls: ['./general-entry-category-master-add-edit.component.scss']
})
export class GeneralEntryCategoryMasterAddEditComponent implements OnInit {
  pageName = 'General Entry Category'
  model = {} as GeneralEntryCategoryPostModel;
  dropDown = new DropDownModel();
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    ImagePath: [undefined],
    IsShowInMain: [undefined],
    IsShowDataInMain: [undefined],
    IsSingleEntry: [undefined],
    IsShowThumbnail: [undefined],
    IsShowUrl: [undefined],
    SortedOrder: [undefined, Validators.required],
    ContentType: [undefined, Validators.required],
  });
  isFileAttached = false;
  get f() { return this.formgrp.controls; }
  @Input() set Id(value: string) {
    this.model.Id = value;
    if (this.model !== null && this.model.Id.length > 0) {
      this.onGetDetail();
    } else {
      this.reset();
    }
  }

  @Output() OnSave = new EventEmitter<boolean>();
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService, private readonly _generalEntryService: GeneralEntryService) {

  }

  ngOnInit(): void {
    this.GetDropDown();
    this._activatedRoute.params.subscribe(x => {
      this.model.Id = this._activatedRoute.snapshot.params.id ? this._activatedRoute.snapshot.params.id : '';
      if (this.model.Id.length > 0) {
        this.onGetDetail();
      }
    });
  }

  onDocumentAttach(file: FileInfo[]) {
    this.model.ImagePath = file[0].FileBase64;
    this.isFileAttached = true;
  }
  RemoveDocument(file: string) {
    this.model.ImagePath = '';
  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlContentType]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlContentType = ddls?.ddlContentType;

      }
    });
  }

  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.SortedOrder = Number(this.model.SortedOrder);
      this._generalEntryService.AddUpdateGeneralEntryCategory(this.model).subscribe(x => {
        if (x.IsSuccess) {
          this.toast.success("General Entry Category added sucessfully...", "Saved");
          this._route.navigate(['./admin/master/general-entry-category']);
          this.OnSave.emit(true);
        } else {
          this.OnSave.emit(false);
          this.toast.error(x.Message as string, "Faild");
        }
      })
    }
  }

  onGetDetail() {

    this._generalEntryService.GetGeneralEntryCategory(this.model.Id).subscribe(response => {
      if (response.IsSuccess) {
        const data = response.Data as GeneralEntryCategoryViewModel;
        this.model.Name = data.Name;
        this.model.ContentType = data.ContentType;
        this.model.ImagePath = data.ImagePath;
        this.model.IsShowInMain = data.IsShowInMain;
        this.model.IsShowDataInMain = data.IsShowDataInMain;
        this.model.IsSingleEntry = data.IsSingleEntry;
        this.model.IsShowThumbnail = data.IsShowThumbnail;
        this.model.IsShowUrl = data.IsShowUrl;
        this.model.SortedOrder = Number(data.SortedOrder);
      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }
  reset() {
    this.model = {} as GeneralEntryCategoryPostModel;
    this.isFileAttached = false;
    this.formgrp.reset();
  }

}
