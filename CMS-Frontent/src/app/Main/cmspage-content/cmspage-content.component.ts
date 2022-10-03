import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { CMSPageService, CMSPageViewModel } from '../../Shared/Services/cmspage.service';

@Component({
  selector: 'app-cmspage-content',
  templateUrl: './cmspage-content.component.html',
  styleUrls: ['./cmspage-content.component.css']
})
export class CMSPageContentComponent implements OnInit {
  recordId: number;
  model = [] as CMSPageViewModel[];

  constructor(private readonly _route: ActivatedRoute, private readonly _sainitizer: DomSanitizer, private _cmsPageService: CMSPageService) {

  }

  ngOnInit(): void {
    this._route.queryParams.subscribe(x => {
      this.recordId = x?.id;
      this.getDetails();
    });
  }

  getDetails() {
    this._cmsPageService.GetDetails(this.recordId).subscribe(res => {
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
