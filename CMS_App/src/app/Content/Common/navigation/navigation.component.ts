import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { environment } from '../../../../environments/environment';
import { CommonService } from '../../../Shared/Services/common.service';
import { DropDown_key } from '../../../Shared/Helper/constants';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }
  dropDown = new DropDownModel();

  constructor(private readonly _authService: AuthService, private readonly _commonService: CommonService) {

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
        this.dropDown.ddlLookupTypeMaster = ddls.ddlLookupTypeMaster;

      }
    });

  }
}
