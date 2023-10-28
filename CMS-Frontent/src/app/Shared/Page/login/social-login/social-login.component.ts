import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService, SocialUser } from 'angularx-social-login';
import { AuthService } from 'src/app/Shared/Services/UserService/auth.service';


@Component({
  selector: 'app-social-login',
  templateUrl: './social-login.component.html',
  styleUrls: ['./social-login.component.css']
})
export class SocialLoginComponent implements OnInit {
  user: SocialUser | undefined;
  loggedIn: boolean | undefined;
  isLoggedin?: boolean = undefined;

  constructor(private router: Router, private Auth: AuthService, private ssoauthService: SocialAuthService) { }
  // npm install angularx-social-login
  ngOnInit(): void {
    this.ssoauthService.authState.subscribe((user) => {
      this.user = user;
      this.loggedIn = (user != null);
      this.isLoggedin = user != null;
      console.log("Login User = " + this.user.name + this.user.email);


    });
  }
  signInWithFB(): void { //Facebook Login
    this.ssoauthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  signWithGoogle(): void {
    console.log("googlesignin"); //for google sign in
    this.ssoauthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    console.log("googlesignin1");
  }
  
  signOut():any{ //for google out
    this.ssoauthService.signOut();
  }
  refreshToken(): void {
    this.ssoauthService.refreshAuthToken(FacebookLoginProvider.PROVIDER_ID);
  }
}
