import { Component, OnInit } from '@angular/core';
import { DropDown_key } from '../../Constant';
import { DropDownItem, DropDownModel } from '../../Helper/Common';
import { CommonService } from '../../Services/Core/common.service';
import { SecurityService } from '../../Services/Core/security.service';
@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  cmsPageMenu: DropDownItem[] = [];
  ddlCategory: DropDownItem[] = [];

  constructor(private readonly _commonService: CommonService, private readonly _securityService: SecurityService) {

    if (this._securityService.getStorage('nav-cms-page-menu')) {
      this.cmsPageMenu = JSON.parse(this._securityService.getStorage('nav-cms-page-menu'));
    }
    if (this._securityService.getStorage('nav-Category')) {
      this.ddlCategory = JSON.parse(this._securityService.getStorage('nav-Category'));
    }
  }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    let itms = [];
    if (this.ddlCategory.length == 0) {
      itms.push(DropDown_key.ddlLookupGroup)
    }
    if (this.cmsPageMenu.length == 0) {
      itms.push(DropDown_key.ddlCMSPage)
    }
    if (itms.length > 0) {
      let serve = this._commonService.GetDropDown([DropDown_key.ddlCategory, DropDown_key.ddlCMSPage], true).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          const ddls = res?.Data as DropDownModel;
          this.cmsPageMenu = ddls.ddlCMSPage?.map(x => { return { Text: x.Text, Value: x.Value } as DropDownItem });
          this.ddlCategory = ddls.ddlCategory?.slice(0, 6).map(x => { return { Text: x.Text, Value: x.Value } as DropDownItem });
          setTimeout(() => {
            this._securityService?.setStorage('nav-cms-page-menu', JSON.stringify(this.cmsPageMenu))
            this._securityService?.setStorage('nav-Category', JSON.stringify(this.ddlCategory))
          }, 10);
        }
      });
    }
  }

}
