import { DropDown_key } from 'src/app/Shared/Constant';
import { Component, OnInit } from '@angular/core';
import { CommonService } from '../../Services/common.service';
import { DropDownModel, GroupDropDownItem } from '../../Helper/Common';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  menuModel: GroupDropDownItem[];
  constructor(private readonly _commonService: CommonService) { }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlLookupGroup], true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        const ddls = res?.Data as DropDownModel;

        this.menuModel = ddls.ddlLookupGroup

      }
    });
  }


}
