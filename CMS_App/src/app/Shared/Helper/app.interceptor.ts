import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoaderService } from './loader.service';
import { SecurityService } from './security.service';

@Injectable()
export class AppInterceptor implements HttpInterceptor {

  private requests: HttpRequest<any>[] = [];

  constructor(private _loaderService: LoaderService, private _commonService: SecurityService) {

  }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    this._loaderService.show();
debugger
    let Token = this._commonService.getStorage("authToken") != null ? this._commonService.getStorage("authToken") : null;

    if (Token != null) {
      req = req.clone({
        setHeaders: {
          Authorization: "Bearer " + Token,

        }
      });
    }

    return next.handle(req).pipe(
      finalize(() => this._loaderService.hide()));

  }
}
