import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { IndexModel } from 'src/app/Shared/Helper/Common';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';
import { UserAddressPostModel, UserAddressService, UserAddressViewModel } from 'src/app/Shared/Services/UserService/user-address.service';
declare var $: any;
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit, AfterViewInit {
  userAddress = [] as UserAddressViewModel[];

  selectedAddress: UserAddressViewModel;
  isAdd = true;
  addressConfig: { isDeleteButton: false }
  constructor(private readonly _authService: AuthService, private readonly _route: Router,
    private _userAddressService: UserAddressService) { }

  ngOnInit(): void {
    this.CheckoutStepper();

  }
  ngAfterViewInit(): void {
    //Called after ngAfterContentInit when the component's view has been initialized. Applies to components only.
    //Add 'implements AfterViewInit' to the class.
    this.checkPrerequisite();

  }
  CheckoutStepper() {
    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    $(".next").click(function () {
      current_fs = $(this).parent();
      next_fs = $(this).parent().next();
      //Add Class Active
      $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
      //show the next fieldset
      next_fs.show();
      //hide the current fieldset with style
      current_fs.animate({ opacity: 0 }, {
        step: function (now) {
          // for making fielset appear animation
          opacity = 1 - now;
          current_fs.css({
            'display': 'none',
            'position': 'relative'
          });
          next_fs.css({ 'opacity': opacity });
        },
        duration: 600
      });
    });
    $(".previous").click(function () {
      current_fs = $(this).parent();
      previous_fs = $(this).parent().prev();
      //Remove class active
      $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");
      //show the previous fieldset
      previous_fs.show();
      //hide the current fieldset with style
      current_fs.animate({ opacity: 0 }, {
        step: function (now) {
          // for making fielset appear animation
          opacity = 1 - now;
          current_fs.css({
            'display': 'none',
            'position': 'relative'
          });
          previous_fs.css({ 'opacity': opacity });
        },
        duration: 600
      });
    });
    $('.radio-group .radio').click(function () {
      $(this).parent().find('.radio').removeClass('selected');
      $(this).addClass('selected');
    });
    $(".submit").click(function () {
      return false;
    })
  }

  checkPrerequisite() {
    if (!this._authService.IsAuthentication.value) {
      this._route.navigate([`/login`], { queryParams: { returnURL: this._route.url }, });
    } else {
      this.getAddress();
    }
  }

  getAddress() {
    const model = new IndexModel();
    model.PageSize = 101;
    this._userAddressService.GetList(model).subscribe(res => {
      if (res.IsSuccess && res.Data.length > 0) {
        this.userAddress = res.Data;
      }
      //else {
      //   this._route.navigate([`/user/address`], { queryParams: { returnURL: this._route.url }, });
      //   }
    })
  }
  onSave(data: UserAddressViewModel) {

    if (data.IsPrimary) {
      this.userAddress.forEach(ele => {
        ele.IsPrimary = false;
      })
    }
    if (this.isAdd) {
      this.userAddress.push(data);
    } else {
      let ind = this.userAddress.findIndex(res => res.Id == data.Id);
      this.userAddress[ind] = data;
    }

  }
}
