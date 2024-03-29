import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SecurityService } from 'src/app/Shared/Services/Core/security.service';
import { CMSPageService, CMSPageViewModel } from '../../Shared/Services/CMSPageService/cmspage.service';

@Component({
  selector: 'app-cmspage-content',
  templateUrl: './cmspage-content.component.html',
  styleUrls: ['./cmspage-content.component.css']
})
export class CMSPageContentComponent implements OnInit {
  recordId: string;
  model = [] as CMSPageViewModel[];

  constructor(private readonly _route: ActivatedRoute, private readonly _securityService: SecurityService,
    private _cmsPageService: CMSPageService) {

  }

  ngOnInit(): void {
    this._route.queryParams.subscribe(x => {
      this.recordId = x?.id ? x?.id : '';
      if (this._securityService?.getStorage(`cms-page-content_${this._securityService.decrypt(this.recordId)}`)) {
        this.model = JSON.parse(this._securityService?.getStorage(`cms-page-content_${this._securityService.decrypt(this.recordId)}`));
      }

      this.getDetails();
    });
  }

  getDetails() {
    this._cmsPageService.GetDetails(this.recordId).subscribe(res => {
      if (res.IsSuccess) {
        const data = res.Data;
        this.model = data.sort(x => x.SortedOrder);
        setTimeout(() => {
          this._securityService?.setStorage(`cms-page-content_${this._securityService.decrypt(this.recordId)}`, JSON.stringify(this.model))
        }, 10);
      }
    },
      error => {
        console.log(error);
      })
  }


}
