import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from '../../Shared/Services/security.service';
import { CMSPageService, CMSPageViewModel } from '../../Shared/Services/cmspage.service';

@Component({
  selector: 'app-cmspage-content',
  templateUrl: './cmspage-content.component.html',
  styleUrls: ['./cmspage-content.component.css']
})
export class CMSPageContentComponent implements OnInit {
  recordId: string;
  model = [] as CMSPageViewModel[];

  constructor(private readonly _route: ActivatedRoute, private readonly _securityService: SecurityService, private _cmsPageService: CMSPageService) {

  }

  ngOnInit(): void {
    this._route.queryParams.subscribe(x => {
      this.recordId = x?.id ? x?.id : '';
      this.getDetails();
    });
  }

  getDetails() {
    this._cmsPageService.GetDetails(Number(this._securityService.decrypt(this.recordId))).subscribe(res => {
      if (res.IsSuccess) {
        debugger

        const data = res.Data;
        this.model = data.sort(x => x.SortedOrder);
      }
    },
      error => {
        console.log(error);
      })
  }


}
