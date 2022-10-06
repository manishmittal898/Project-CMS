import { CMSPagePostModel } from './../../../../../Shared/Services/cmspage-master.service';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EditorConfig, Message } from './../../../../../Shared/Helper/constants';
import { CMSPageMasterService } from 'src/app/Shared/Services/cmspage-master.service';
import { CommonService } from 'src/app/Shared/Services/common.service';

@Component({
  selector: 'app-cmspage-add-update',
  templateUrl: './cmspage-add-update.component.html',
  styleUrls: ['./cmspage-add-update.component.scss']
})
export class CMSPageAddUpdateComponent implements OnInit {
  id: number = 0;
  pageName: string = '';
  model = {} as CMSPagePostModel;
  postModel = [] as CMSPagePostModel[];
  editorConfig = EditorConfig.Config;
  formgrp = this.fb.group({
    Heading: [undefined, Validators.required],
    Content: [undefined, Validators.required],
    SortedOrder: [undefined, Validators.required],

  });
  get f() { return this.formgrp.controls; }

  constructor(private readonly fb: FormBuilder, private _router: Router, private _activatedRoute: ActivatedRoute, readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _cmsPageService: CMSPageMasterService) {
    _activatedRoute.params.subscribe(x => {
      this.pageName = this._activatedRoute.snapshot.params.name.split('_').join(' ');
    })

    _activatedRoute.queryParamMap.subscribe(x => {
      this.id = Number(x.get('id'))
    });

  }

  ngOnInit(): void {
    this.getDetails();
  }

  AddItem() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      if (this.model.Id < 1 || typeof this.model.Id == undefined) {
        this.postModel.push(this.model);
      } else {
        const idx = this.postModel.findIndex(x => x.Id == this.model.Id);
        if (idx >= 0) {
          this.postModel[idx] = this.model;
        }
      }
    } else {
      this.toast.warning(Message.VerifyInput);

    }

  }

  SaveData() {

    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.Id = this.model.Id > 0 ? this.model.Id : 0;
      this.model.PageId = this.id;
      this.model.SortedOrder = Number(this.model.SortedOrder);
      this._cmsPageService.AddUpdateCMSPage(this.model).subscribe(res => {

        if (res.IsSuccess) {
          this.model.Id = Number(res.Data);
          this.postModel.push(this.model);
          this.model = {} as CMSPagePostModel;
          this.formgrp.reset();
          this.toast.success(res.Message as string);
        } else {
          this.toast.error(res.Message as string);

        }
      })
    }

  }

  getDetails() {

    this._cmsPageService.GetDetails(this.id).subscribe(res => {

      if (res.IsSuccess) {
        const data = res.Data;
        this.postModel = [];
        data?.forEach(item => {
          const dtItem = {
            Content: item.Content,
            Heading: item.Heading,
            Id: item.Id,
            SortedOrder: item.SortedOrder
          } as CMSPagePostModel;
          this.postModel.push(dtItem);
        })
      }
    },
      error => {

        console.log(error);
      })
  }

  editItem(item: CMSPagePostModel) {
    const idx = this.postModel.findIndex(x => x.Id === item.Id);
    if (idx >= 0) {
        this.postModel.splice(idx, 1);
    }


    this.model = Object.assign({}, item);
  }

  deleteItem(item: CMSPagePostModel) {
    this._commonService.Question(Message.DeleteConfirmation as string).then(isTrue => {
      if (isTrue) {

        const idx = this.postModel.findIndex(x => x.Id === item.Id);
        if (idx >= 0) {
          this._cmsPageService.DeleteCMSContent(item.Id).subscribe(res => {
            if (res.IsSuccess) {
              this.postModel.splice(idx, 1);
              this.toast.success(Message.DeleteSuccess as string);

            }
          },
            error => {
              console.log(error);
              this.toast.error(Message.DeleteFail as string);
            })


        }
      }
    });
  }

  onCancel() {
    this.model = {} as CMSPagePostModel;
    this.formgrp.reset();
  }

}
