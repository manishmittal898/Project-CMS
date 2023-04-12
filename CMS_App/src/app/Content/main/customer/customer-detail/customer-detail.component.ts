import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { CustomerService } from 'src/app/Shared/Services/customer.service';
import { UserMasterViewModel } from 'src/app/Shared/Services/user.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit {
  model?: UserMasterViewModel;
  id!: number;
  constructor(private _activatedRoute: ActivatedRoute, private readonly toast: ToastrService, private _userService: CustomerService) {
    this._activatedRoute.params.subscribe(x => {
      this.id = this._activatedRoute.snapshot.params.id;
    });
  }

  ngOnInit(): void {
    this.getDetail();
  }
  getDetail(): void {
    this._userService.GetCustomerDetail(this.id).subscribe(response => {
      if (response.IsSuccess) {
        debugger
        this.model = response.Data as UserMasterViewModel;

      } else {
        this.toast.error(response.Message?.toString(), 'Error');
      }
    },
      error => {
      });
  }

}
