import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Services/Core/common.service';
import { DropDownItem, DropDownModel, GroupDropDownItem } from '../../Helper/Common';
import { x64 } from 'crypto-js';
import { AuthService } from '../../Services/UserService/auth.service';
import { SecurityService } from '../../Services/Core/security.service';
import { DropDown_key } from '../../Constant';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})

export class NavBarComponent implements OnInit {
  isLoggedIn = false;
  menuModel: GroupDropDownItem[];
  cmsPageMenu: DropDownItem[];
  constructor(private readonly _commonService: CommonService, private readonly _securityService: SecurityService, private readonly _authService: AuthService,) {
    if (this._securityService.getStorage('nav-collections-menu')) {
      this.menuModel = JSON.parse(this._securityService.getStorage('nav-collections-menu'));
    }
    if (this._securityService.getStorage('nav-cms-page-menu')) {
      this.cmsPageMenu = JSON.parse(this._securityService.getStorage('nav-cms-page-menu'));

    }
  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();
    this.GetDropDown();
    this._authService.IsAuthentication.subscribe(x => {
      this.isLoggedIn = x as boolean ?? false;
      if (!this.isLoggedIn) {
        this.logout();
      }
    });
  }

  GetDropDown() {
    const serve = this._commonService.GetDropDown([DropDown_key.ddlLookupGroup, DropDown_key.ddlCMSPage], true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;
        this.menuModel = ddls.ddlLookupGroup.map(x => {
          return {
            Category: x.Category, CategoryId: this._securityService.encrypt(String(x.CategoryId)),
            Data: x.Data.map(sd => { return { Text: sd.Text, Value: this._securityService.encrypt(String(sd.Value)), Category: sd.Category, CategoryId: this._securityService.encrypt(String(sd.CategoryId)) } })
          } as any
        });
        this.cmsPageMenu = ddls?.ddlCMSPage?.map(x => { return { Text: x.Text, Value: this._securityService.encrypt(String(x.Value)) } as DropDownItem });
        this._securityService.setStorage('nav-collections-menu', JSON.stringify(this.menuModel));
        this._securityService.setStorage('nav-cms-page-menu', JSON.stringify(this.cmsPageMenu));
      }
    });
  }


  logout() {
    this._authService.LogOut();
  }

}
