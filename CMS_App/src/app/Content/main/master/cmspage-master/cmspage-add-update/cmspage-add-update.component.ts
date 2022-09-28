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
      this.pageName = this._activatedRoute.snapshot.params.name;
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
    debugger
    this.model.PageId = this.id;
    this.model.SortedOrder = Number(this.model.SortedOrder);

    this._cmsPageService.AddUpdateCMSPage(this.model).subscribe(res => {
      debugger
      if (res.IsSuccess) {
        this.model.Id = Number(res.Data);
        this.postModel.push(this.model);
        this.toast.success(res.Message as string);
        // this._router.navigate([`admin/master/cms-page`]);
      } else {
        this.toast.error(res.Message as string);

      }
    })
  }

  getDetails() {
    
    this._cmsPageService.GetDetails(this.id).subscribe(res => {
      debugger
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
        debugger
        console.log(error);
      })
  }

  editItem(item: CMSPagePostModel) {
    this.model = item;
  }



}
