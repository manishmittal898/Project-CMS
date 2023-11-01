import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';
import { environment } from 'src/environments/environment';
declare var google: any
declare var handleGoogleLoginResponse: any;
@Component({
  selector: 'app-social-login',
  templateUrl: './social-login.component.html',
  styleUrls: ['./social-login.component.css']
})
export class SocialLoginComponent implements OnInit {
  user: SocialUser | undefined;
  loggedIn: boolean | undefined;
  isLoggedin?: boolean = undefined;
  googleClientId = environment.GoogleClientId;
  constructor(private router: Router, private Auth: AuthService, private ssoAuthService: SocialAuthService) { }
  // npm install angularx-social-login
  
  ngOnInit(): void {
    this.ssoAuthService.authState.subscribe((user) => {
      if (user) {

        this.user = user;
        this.loggedIn = (user != null);
        this.isLoggedin = user != null;
        console.log("Login User = " + this.user.name + this.user.email);
      }


    });

  }
  ngAfterViewInit(): void {
    this.googleLoginInit();
  }

  googleLoginInit() {
    google.accounts.id.initialize({
      client_id: environment.GoogleClientId,
      callback: handleGoogleLoginResponse
    });
    setTimeout(() => {
      google.accounts.id.renderButton(
        document.getElementById("btnGoogleLogin"),
        { theme: "outline", size: "large" }  // customization attributes
      );
      google.accounts.id.prompt(); // also display the One Tap dialog
    }, 100);

  }

  signInWithFB(): void { //Facebook Login
    this.ssoAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  refreshToken(): void {
    this.ssoAuthService.refreshAuthToken(FacebookLoginProvider.PROVIDER_ID);
  }
}
