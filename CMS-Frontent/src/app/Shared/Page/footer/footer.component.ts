import { Component, OnInit } from '@angular/core';
import { DropDown_key } from '../../Constant';
import { DropDownItem, DropDownModel } from '../../Helper/Common';
import { CommonService } from '../../Services/common.service';
import { SecurityService } from '../../Services/security.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  cmsPageMenu: DropDownItem[];
  constructor(private readonly _commonService: CommonService, private readonly _securityService: SecurityService) {

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
        debugger
        const ddls = res?.Data as DropDownModel;

        this.cmsPageMenu = ddls.ddlCMSPage;

        this._securityService.setStorage('nav-cms-page-menu', JSON.stringify(this.cmsPageMenu))


      }
    });
  }

}
