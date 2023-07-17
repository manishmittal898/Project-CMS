import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthService, LoginUserDetailModel } from '../../Services/UserService/auth.service';
import { Routing_Url } from '../../Constant';
import { AccountService } from '../../Services/UserService/account.service';
import { SecurityService } from '../../Services/Core/security.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isShowPass = false;
  model: any = {};
  get routing_Url() { return Routing_Url };

  constructor(private readonly _accountService :AccountService,private readonly _authService: AuthService, private readonly _security: SecurityService,
    private readonly _route: Router, private readonly toast: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {

    this.model.Plateform = "Customer";
    if (this.model.Email == undefined || this.model.Password == undefined) {
      this.toast.warning('Please enter username and password', 'Required');
      return;
    }
    if (!environment.IsAutoLogin) {
      this._security.GetEncrptedText(this.model.Password).then(x => {
        if (x.IsSuccess) {
          const encPass = x.Data;
          const postModel = {
            Email: this.model.Email,
            Password: encPass,
            Plateform: this.model.Plateform
          };
          this._accountService.Login(postModel).subscribe((res) => {
            if (res.IsSuccess) {
              debugger
              let data = res.Data as LoginUserDetailModel;
              this._authService.SaveUserToken(data.Token);
              this._authService.SaveUserDetail(data);
              this.toast.success(res.Message?.toString(), 'Login');
              this._route.navigate(['/user']);
              this._authService.IsAuthenticate()
            } else {
              this.toast.info(res.Message?.toString(), 'Login');
            }
          });
        }
      });
    } else {
      this._authService.SaveUserToken("testtoken");
      this._route.navigate(['']);
    }
  }

  forgetPassword(){}
}
