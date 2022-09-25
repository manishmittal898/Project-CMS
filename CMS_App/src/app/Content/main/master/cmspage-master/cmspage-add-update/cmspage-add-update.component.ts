import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private _router: Router, private _activatedRoute: ActivatedRoute, private readonly _commonService: CommonService,
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
