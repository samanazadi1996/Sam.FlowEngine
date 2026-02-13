import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private authenticationService: AuthenticationService) {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authReq = req;

    if (this.authenticationService.isLoggedIn) {
      const token = this.authenticationService.getToken();

      if (token) {
        authReq = req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        });
      }
    }
    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401 || error.status === 403) {
          this.authenticationService.logout('/account/login');
        }

        return throwError(() => error);
      })
    );
  }
}
