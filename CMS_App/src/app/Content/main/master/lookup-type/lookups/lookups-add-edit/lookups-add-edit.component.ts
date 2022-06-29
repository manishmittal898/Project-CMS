import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { LookupMasterModel } from '../../../../../../Shared/Services/Master/lookup.service';
import { CommonService } from '../../../../../../Shared/Services/common.service';

@Component({
  selector: 'app-lookups-add-edit',
  templateUrl: './lookups-add-edit.component.html',
  styleUrls: ['./lookups-add-edit.component.scss']
})
export class LookupsAddEditComponent implements OnInit {
  dropDown = new DropDownModel();
  model = {} as LookupMasterModel;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    SortedOrder: [undefined, Validators.required],
    ImagePath: [undefined, Validators.required],
  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  constructor(public dialogRef: MatDialogRef<LookupsAddEditComponent>, private readonly fb: FormBuilder, public _commonService: CommonService,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string, Heading: string }) { }

  ngOnInit(): void {
  }

  onClose() {
    this.dialogRef.close(true);
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
  }

  RemoveDocument(file: string) { }
  onDocumentAttach(file: any) { }
}
