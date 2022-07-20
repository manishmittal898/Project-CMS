import { Component, OnInit, Inject } from "@angular/core";
import { Validators, FormBuilder } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";
import { DropDown_key } from "src/app/Shared/Helper/constants";
import { FileInfo } from "src/app/Shared/Helper/shared/file-selector/file-selector.component";
import { CommonService } from "src/app/Shared/Services/common.service";
import { SubLookupMasterPostModel, SubLookupService } from "src/app/Shared/Services/Master/sub-lookup.service";


@Component({
  selector: 'app-sub-lookup-add-edit',
  templateUrl: './sub-lookup-add-edit.component.html',
  styleUrls: ['./sub-lookup-add-edit.component.scss']
})
export class SubLookupAddEditComponent implements OnInit {


  model = {} as SubLookupMasterPostModel;
  isFileAttached=false;
  formgrp = this.fb.group({
    Name: [undefined, Validators.required],
    SortedOrder: [undefined, Validators.required],
    ImagePath: [undefined],
  });
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  get getFileName() { return this.model.ImagePath ? this.model.ImagePath.split('/')[this.model.ImagePath.split('/').length - 1] : '' }
  constructor(public dialogRef: MatDialogRef<SubLookupAddEditComponent>, private readonly _lookupService: SubLookupService,
    private readonly fb: FormBuilder, public _commonService: CommonService, private readonly toast: ToastrService,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: number, Heading: string }) { }

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
      this.model.LookUpId = Number(this.data.Type);
      this.model.Id = this.data.Id;
      this.model.SortedOrder= Number(this.model.SortedOrder),
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
          LookUpId: Number(x.Data?.LookUpId),
        } as SubLookupMasterPostModel;

        this.isFileAttached=this.model.ImagePath?false:this.isFileAttached;
      }
    })
  }
  onDocumentAttach(file: FileInfo[]) {
    this.model.ImagePath = file[0].FileBase64;
    this.isFileAttached=true;
  }

}
