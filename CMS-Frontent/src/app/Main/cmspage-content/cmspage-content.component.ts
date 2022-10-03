import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-cmspage-content',
  templateUrl: './cmspage-content.component.html',
  styleUrls: ['./cmspage-content.component.css']
})
export class CMSPageContentComponent implements OnInit {
  recordId: number;

  constructor(private readonly _route: ActivatedRoute, private readonly _sainitizer: DomSanitizer) {

  }

  ngOnInit(): void {
    this._route.queryParams.subscribe(x => {
      debugger
      this.recordId = x.id;

    });
  }

}
