import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { LookupMasterPostModel, LookupService } from '../../../../../../Shared/Services/Master/lookup.service';
import { CommonService } from '../../../../../../Shared/Services/common.service';
import { ToastrService } from 'ngx-toastr';
import { FileInfo } from 'src/app/Shared/Helper/shared/file-selector/file-selector.component';
import { LookupTypeService } from 'src/app/Shared/Services/Master/lookup-type.service';

@Component({
  selector: 'app-lookups-add-edit',
  templateUrl: './lookups-add-edit.component.html',
  styleUrls: ['./lookups-add-edit.component.scss']
})
export class LookupsAddEditComponent implements OnInit {
  dropDown = new DropDownModel();
  model = {} as LookupMasterPostModel;
  isFileAttached = false;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    SortedOrder: [undefined, Validators.required],
    ImagePath: [undefined, Validators.required],
  });
  iSImageVisible = false;
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' }
  constructor(public dialogRef: MatDialogRef<LookupsAddEditComponent>, private readonly _lookupService: LookupService, private readonly _lookupTypeService: LookupTypeService,
    private readonly fb: FormBuilder, public _commonService: CommonService, private readonly toast: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: number, Heading: string }) {

    this._lookupTypeService.GetLookupTypeMaster(this.data.Type).subscribe(x => {
      debugger
      if (x.IsSuccess) {
        if (!x.Data?.IsImage) {

          this.formgrp.get('ImagePath')?.clearValidators();
          this.formgrp.get('ImagePath')?.updateValueAndValidity();
          this.iSImageVisible = false;
        } else {
          this.iSImageVisible = true;
          this.formgrp.get('ImagePath')?.setValidators([Validators.required]);
          this.formgrp.get('ImagePath')?.updateValueAndValidity();
        }
      }
    })
  }

  ngOnInit(): void {
    if (this.data.Id > 0) {
      this.getDetail();
    }
  }

  onClose(result: any = null) {
    this.dialogRef.close(result);
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.LookUpType = Number(this.data.Type);
      this.model.Id = this.data.Id;
      this.model.SortedOrder = Number(this.model.SortedOrder);

      this._lookupService.AddUpdateLookupMaster(this.model).subscribe(x => {
        if (x.IsSuccess) {
          this.toast.success(x.Message as string);
          this.onClose(true);
        } else {
          this.toast.error(x.Message as string);
          this.onClose(false);
        }


      }, error => { });

    }
  }

  RemoveDocument(file: string) {
    this.model.ImagePath = '';
  }
  getDetail() {
    this._lookupService.GetLookupMaster(this.data.Id).subscribe(x => {
      if (x.IsSuccess) {
        this.model = {
          Id: this.data.Id,
          Name: x.Data?.Name,
          ImagePath: x.Data?.ImagePath,
          SortedOrder: Number(x.Data?.SortedOrder),
          LookUpType: Number(x.Data?.LookUpType),
        } as LookupMasterPostModel;

        this.isFileAttached = this.model.ImagePath ? false : this.isFileAttached;
      }
    })
  }
  onDocumentAttach(file: FileInfo[]) {
    this.model.ImagePath = file[0].FileBase64;
    this.isFileAttached = true;
  }
}
