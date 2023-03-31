import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { environment } from '../../../../environments/environment';
import { CommonService } from '../../../Shared/Services/common.service';
import { DropDown_key } from '../../../Shared/Helper/constants';
import { DropDownModel, MenuModel } from 'src/app/Shared/Helper/common-model';
import { LookupTypeEnum } from '../../../Shared/Enum/fixed-value';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }
  dropDown = new DropDownModel();
  menuModel!: MenuModel[];
  constructor(private readonly _authService: AuthService, private readonly _commonService: CommonService) {
    this.menuModel = [
      { Name: "Dashboard", Icon: "bi bi-house-door", Url: "/admin" },
      {
        Name: "Product", Icon: "bi bi-columns-gap", children: [
          { Name: "List", Icon: "bi bi-list-check", Url: "./product/list" },
          { Name: "Add", Icon: "bi bi-plus-circle", Url: "./product/add" }]
      },
      {
        Name: "Master", Icon: "bi bi-box-seam", children: []
      },
      {
        Name: "CMS Page", Icon: "bi bi-file-earmark-break", children: [
          { Name: "List", Icon: "bi bi-list-check", Url: "./master/cms-page" },
        ]
      },
      {
        Name: "General Entry", Icon: "bi bi-file-earmark-break", children: [
          { Name: "List", Icon: "bi bi-view-stacked", Url: "./master/general-entry" },
          { Name: "Category Master", Icon: "bi bi-gear-wide-connected", Url: "./master/general-entry-category" },

        ]
      },
    ]
  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();
    this.getLookupTypes();
    this._authService.IsAuthentication.subscribe(x => {

    });
  }

  getLookupTypes() {
    this._commonService.GetDropDown([DropDown_key.ddlLookupTypeMasters]).subscribe(res => {
      if (res.IsSuccess) {

        let ddls = res.Data as DropDownModel;
        this.dropDown.ddlLookupTypeMaster = ddls.ddlLookupTypeMaster.filter(x => ![LookupTypeEnum.CMS_Page, LookupTypeEnum.State, LookupTypeEnum.Address_Type].includes(Number(x.Value)));
        let idx = this.menuModel.findIndex(x => x.Name === 'Master');
        this.dropDown.ddlLookupTypeMaster.forEach(element => {
          this.menuModel[idx].children?.push({ Name: element.Text, Icon: "bi bi-house-door", Url: `./master/${element.Text}/${element.Value}` });
        });
      }
    });

  }
}
