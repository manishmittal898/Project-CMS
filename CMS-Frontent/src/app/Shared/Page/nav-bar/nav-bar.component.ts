import { DropDown_key } from 'src/app/Shared/Constant';
import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Services/common.service';
import { DropDownItem, DropDownModel, GroupDropDownItem } from '../../Helper/Common';
import { SecurityService } from '../../Services/security.service';
import { x64 } from 'crypto-js';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})

export class NavBarComponent implements OnInit {
  menuModel: GroupDropDownItem[];
  cmsPageMenu: DropDownItem[];
  constructor(private readonly _commonService: CommonService, private readonly _securityService: SecurityService) {
    if (this._securityService.getStorage('nav-collections-menu')) {
      this.menuModel = JSON.parse(this._securityService.getStorage('nav-collections-menu'));
    }
    if (this._securityService.getStorage('nav-cms-page-menu')) {
      this.cmsPageMenu = JSON.parse(this._securityService.getStorage('nav-cms-page-menu'));

    }
  }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlLookupGroup, DropDown_key.ddlCMSPage], true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        debugger
        this.menuModel = ddls.ddlLookupGroup;
        this.cmsPageMenu = ddls.ddlCMSPage?.map(x => { return { Text: x.Text, Value: this._securityService.encrypt(String(x.Value)) } as DropDownItem });

        this._securityService.setStorage('nav-collections-menu', JSON.stringify(this.menuModel))
        this._securityService.setStorage('nav-cms-page-menu', JSON.stringify(this.cmsPageMenu))
      }
    });
  }

}
