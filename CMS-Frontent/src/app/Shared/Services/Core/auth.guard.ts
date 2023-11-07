import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../UserService/auth.service';
import { Routing_Url } from '../../Constant';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private readonly _router: Router, private readonly auth: AuthService) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (this.auth.IsAuthentication.value == null) {
      this.auth.IsAuthenticate()
    }
    if (this.auth.IsAuthentication.value) {
      return true;
    }
    else {
      this._router.navigate([`/${Routing_Url.LoginUrl}`], { queryParams: { returnURL: state.url }, });
      return false;
    }

  }

}
