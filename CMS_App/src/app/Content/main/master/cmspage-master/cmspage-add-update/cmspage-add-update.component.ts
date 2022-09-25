import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EditorConfig } from './../../../../../Shared/Helper/constants';
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
  model={} as { Heading: string, Content: string };
  editorConfig = EditorConfig.Config;
  formgrp = this.fb.group({
    Heading: [undefined, Validators.required],
    Content: [undefined, Validators.required],

  });
  get f() { return this.formgrp.controls; }

  constructor(private readonly fb: FormBuilder, private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private _cmsPageService: CMSPageMasterService,) {
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

}
