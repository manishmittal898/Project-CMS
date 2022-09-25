import { CMSPagePostModel } from './../../../../../Shared/Services/cmspage-master.service';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EditorConfig, Message } from './../../../../../Shared/Helper/constants';
import { CMSPageDataModel, CMSPageMasterService } from 'src/app/Shared/Services/cmspage-master.service';
import { CommonService } from 'src/app/Shared/Services/common.service';

@Component({
  selector: 'app-cmspage-add-update',
  templateUrl: './cmspage-add-update.component.html',
  styleUrls: ['./cmspage-add-update.component.scss']
})
export class CMSPageAddUpdateComponent implements OnInit {
  id: number = 0;
  pageName: string = '';
  model = {} as CMSPageDataModel;
  postModel = {} as CMSPagePostModel;
  editorConfig = EditorConfig.Config;
  formgrp = this.fb.group({
    Heading: [undefined, Validators.required],
    Content: [undefined, Validators.required],
    SortedOrder: [undefined, Validators.required],

  });
  get f() { return this.formgrp.controls; }

  constructor(private readonly fb: FormBuilder, private _router: Router, private _activatedRoute: ActivatedRoute, readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _cmsPageService: CMSPageMasterService) {
    debugger

    _activatedRoute.params.subscribe(x => {
      debugger
      this.pageName = this._activatedRoute.snapshot.params.name;
    })

    _activatedRoute.queryParamMap.subscribe(x => {
      this.id = Number(x.get('id'))
    });

  }

  ngOnInit(): void {

  }

  AddItem() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      if (this.model.Id < 1 || typeof this.model.Id == undefined) {
        this.postModel.Data.push(this.model);
      } else {
        const idx = this.postModel.Data.findIndex(x => x.Id == this.model.Id);
        if (idx >= 0) {
          this.postModel.Data[idx] = this.model;
        }
      }
    }else{
      this.toast.warning(Message.VerifyInput);

    }

  }

  SaveData() {
    this.postModel.PageId == this.id;
    this._cmsPageService.AddUpdateCMSPage(this.postModel).subscribe(res => {
      this.toast.success(res.Message as string);
    })
  }

  

}
