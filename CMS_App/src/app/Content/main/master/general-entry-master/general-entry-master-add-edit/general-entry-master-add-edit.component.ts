import { Component, OnInit } from "@angular/core";
import { Validators, FormBuilder } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { DropDownModel } from "src/app/Shared/Helper/common-model";
import { EditorConfig, DropDown_key } from "src/app/Shared/Helper/constants";
import { FileInfo } from "src/app/Shared/Helper/shared/file-selector/file-selector.component";
import { CommonService } from "src/app/Shared/Services/common.service";
import { GeneralEntryPostModel, GeneralEntryService, GeneralEntryViewModel } from "src/app/Shared/Services/Master/general-entry.service";

@Component({
  selector: 'app-general-entry-master-add-edit',
  templateUrl: './general-entry-master-add-edit.component.html',
  styleUrls: ['./general-entry-master-add-edit.component.scss']
})
export class GeneralEntryMasterAddEditComponent implements OnInit {

  pageName = 'General Entry'
  model = {} as GeneralEntryPostModel;
  dropDown = new DropDownModel();
  formgrp = this.fb.group({
    Category: [undefined, Validators.required], //done
    Title: [undefined, Validators.required],//done
    Description: [undefined],//done
    ImagePath: [undefined],//done
    Keyword: [undefined],
    SortedOrder: [undefined, Validators.required],
    Data: []
  });
  editorConfig = EditorConfig.Config;
  isFileAttached = false;
  get f() { return this.formgrp.controls; }
  get selectedCategory() { return this.model.CategoryId > 0 ? this.dropDown.ddlGeneralEntryCategory.find(x => x.Value == this.model.CategoryId.toString()) : undefined }
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService, private readonly _generalEntryService: GeneralEntryService) {

  }

  ngOnInit(): void {
    this.GetDropDown();
    this._activatedRoute.params.subscribe(x => {
      this.model.Id = this._activatedRoute.snapshot.params.id ? Number(this._activatedRoute.snapshot.params.id) : 0;
      if (this.model.Id > 0) {
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
    debugger
    let serve = this._commonService.GetDropDown([DropDown_key.ddlGeneralEntryCategory], false).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlGeneralEntryCategory = ddls?.ddlGeneralEntryCategory;

      }
    });
  }

  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {

      this._generalEntryService.AddUpdateGeneralEntry(this.model).subscribe(x => {
        if (x.IsSuccess) {
          this.toast.success("General Entry added sucessfully...", "Saved");
          this._route.navigate(['./admin/master/general-entry']);
        } else {
          this.toast.error(x.Message as string, "Faild");
        }
      })
    }
  }

  onGetDetail() {
    this._generalEntryService.GetGeneralEntry(this.model.Id).subscribe(response => {
      if (response.IsSuccess) {
        const data = response.Data as GeneralEntryViewModel;
        this.model.Id = data.Id;
        this.model.CategoryId = data.CategoryId;
        this.model.Title = data.Title;
        this.model.Description = data.Description;
        this.model.SortedOrder = data.SortedOrder;
        this.model.ImagePath = data.ImagePath;
        this.model.Data = data.Data?.map(r => { return r.Value }) as string[];
        this.model.Keyword = data.Keyword;
      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }

}
