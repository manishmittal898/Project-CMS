import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Shared/Helper/auth.service";
import { environment } from "src/environments/environment";
import { Routing_Url } from 'src/app/Shared/Helper/constants';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  isShowPass = false;
  model = {} as any;
  get routing_Url() { return Routing_Url };

  constructor(private readonly _authService: AuthService,
    private readonly _route: Router, private readonly toast: ToastrService
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.model.Plateform = "Web";
    if (this.model.MobileNumber == undefined || this.model.Password == undefined) {
      this.toast.warning('Please enter username and password', 'Required');
      return;
    }
    if (!environment.IsAutoLogin) {
      this._authService.Login(this.model).subscribe((res) => {
        if (res.IsSuccess) {
          let data = res.Data as any;
          this._authService.SaveUserToken(data.Token);
          this._authService.SaveUserDetail(data);
       

          this.toast.success(res.Message?.toString(), 'Login Response');
        } else {
          this.toast.info(res.Message?.toString(), 'Login Response');
        }
      });
    } else {
      this._authService.SaveUserToken("testtoken");
      this._route.navigate(['']);
    }
  }
}
