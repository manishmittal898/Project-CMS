import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GeneralEntryCategoryPostModel } from 'src/app/Shared/Services/Master/general-entry-service.service';

@Component({
  selector: 'app-general-entry-category-master-add-edit',
  templateUrl: './general-entry-category-master-add-edit.component.html',
  styleUrls: ['./general-entry-category-master-add-edit.component.scss']
})
export class GeneralEntryCategoryMasterAddEditComponent implements OnInit {
  pageName = 'General Entry Category'
  model = {} as GeneralEntryCategoryPostModel;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    ImagePath: [undefined, Validators.required],
    IsShowInMain: [undefined],
    IsShowDataInMain: [undefined],
    IsSingleEntry: [undefined],
    SortedOrder: [undefined, Validators.required]
  });
  isFileAttached = false;
  get f() { return this.formgrp.controls; }
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService,) { }

  ngOnInit(): void {
  }

  onDocumentAttach(file: FileInfo[]) {
    this.model.ImagePath = file[0].FileBase64;
    this.isFileAttached = true;
  }
  RemoveDocument(file: string) {
    this.model.ImagePath = '';
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      
    }
  }

}
