import { Component, OnInit } from "@angular/core";
import { Validators, FormBuilder } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { ContentTypeEnum } from "src/app/Shared/Enum/fixed-value";
import { DropDownModel } from "src/app/Shared/Helper/common-model";
import { EditorConfig, DropDown_key, Message } from "src/app/Shared/Helper/constants";
import { FileInfo } from "src/app/Shared/Helper/shared/file-selector/file-selector.component";
import { CommonService } from "src/app/Shared/Services/common.service";
import { GeneralEntryDataViewModel, GeneralEntryPostModel, GeneralEntryService, GeneralEntryViewModel } from "src/app/Shared/Services/Master/general-entry.service";

@Component({
  selector: 'app-general-entry-master-add-edit',
  templateUrl: './general-entry-master-add-edit.component.html',
  styleUrls: ['./general-entry-master-add-edit.component.scss']
})
export class GeneralEntryMasterAddEditComponent implements OnInit {

  pageName = 'General Entry'
  model = {} as GeneralEntryPostModel;
  dataItems: GeneralEntryDataViewModel[] = [];

  fileSelector: any
  dropDown = new DropDownModel();
  formgrp = this.fb.group({
    Category: [undefined, Validators.required], //done
    Title: [undefined, Validators.required],//done
    Description: [undefined],//done
    ImagePath: [undefined],//done
    Url: [undefined],
    Keyword: [undefined],
    SortedOrder: [undefined, Validators.required],
    DataItems: [undefined]
  });
  editorConfig = EditorConfig.Config;
  isFileAttached = false;
  contentTypeEnum = ContentTypeEnum;
  get f() { return this.formgrp.controls; }
  get selectedCategory() { return this.model?.CategoryId?.length > 0 ? this.dropDown.ddlGeneralEntryCategory.find(x => x.Value == this.model.CategoryId.toString()) : undefined }
  constructor(private readonly fb: FormBuilder, private _route: Router, private _activatedRoute: ActivatedRoute,
    public _commonService: CommonService, private readonly toast: ToastrService, private readonly _generalEntryService: GeneralEntryService) {

  }
  get acceptedFiles() {
    return this.selectedCategory?.ContentType == (this.contentTypeEnum.Photo).toString() || this.selectedCategory?.ContentType == (this.contentTypeEnum.MultipleImages).toString() ? '.jpeg,.gif,.png,.jpg,.webp' :
      this.selectedCategory?.ContentType == (this.contentTypeEnum.Document).toString() ? '.doc,.docx,.ppt,.pptx,.pdf,.xlx,.xlsx,.txt' :
        this.selectedCategory?.ContentType == (this.contentTypeEnum.Video).toString() ? '.mp4,.mkv,.avi' : ''
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

    let serve = this._commonService.GetDropDown([DropDown_key.ddlGeneralEntryCategory], false).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
                const ddls = res?.Data as DropDownModel;
        this.dropDown.ddlGeneralEntryCategory = ddls?.ddlGeneralEntryCategory;

      }
    });
  }

  deleteItems(id: string) {
    this._commonService.Question(Message.DeleteConfirmation as string).then(result => {
      if (result) {
        let subscription = this._generalEntryService.DeleteGeneralEntryItems(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              const idx = this.dataItems.findIndex(x => x.Id == id);
              this.dataItems.splice(idx, 1);
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }

  getFileType(fileName: string) {

    const ext = fileName.split('.')[fileName.split('.').length - 1].toLowerCase();
    if (['doc', 'docx', 'ppt', 'pptx', 'pdf', 'txt', 'xlx', 'xlsx'].some(x => x.toLowerCase() === ext)) {
      return 'doc';
    } else if (['jpeg', 'gif', 'png', 'jpg', 'svg','webp'].some(x => x.toLowerCase() === ext)) {
      return 'image';
    }
    else if (['mp4', 'mkv', 'avi',].some(x => x.toLowerCase() === ext)) {
      return 'video';
    } else {
      return ext;
    }

  }

  onFileAttach(file: FileInfo[]) {

    if (file.length > 0) {
      if (!this.model.Data) {
        this.model.Data = [];
      }
      this.model.Data = file.map(x => { return x.FileBase64 });

    } else {
      this.model.Data = [];
    }
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.Data = this.model.Data ? (Array.isArray(this.model.Data) ? this.model.Data : [this.model.Data]) as string[] : this.model.Data;
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
    this._generalEntryService.GetGeneralEntry(this.model.Id, true).subscribe(response => {
      if (response.IsSuccess) {
        
        const data = response.Data as GeneralEntryViewModel;
        this.model.Id = data.Id;
        this.model.CategoryId = data.CategoryId;
        this.model.Title = data.Title;
        this.model.Description = data.Description;
        this.model.SortedOrder = data.SortedOrder;
        this.model.ImagePath = data.ImagePath;
        this.model.Url = data.Url;
        this.dataItems = data.Data;//?.map(r => { return r.Value }) as string[];
        this.model.Keyword = data.Keyword;
      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }

}
