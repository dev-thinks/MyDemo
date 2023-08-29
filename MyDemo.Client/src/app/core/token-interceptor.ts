import { Injectable } from '@angular/core';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { TokenService } from '../services/token.service';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private tokenService: TokenService,
    private router: Router
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const handler = () => {
      //check the url if login then just redirect to user management page after login
      if (this.router.url.includes('/login')) {
        this.router.navigateByUrl('/user-management');
      }
    };

    if (this.tokenService.valid()) {
      //if the token is valid, then append to the http header for each request
      return next
        .handle(
          request.clone({
            headers: request.headers.append('Authorization', this.tokenService.getBearerToken()),
            withCredentials: true,
          })
        )
        .pipe(
          catchError((error: HttpErrorResponse) => {
            //error handler
            if (error.status === 401) {
              this.tokenService.clear();
            }
            return throwError(error);
          }),
          tap(() =>{
            handler();})
        );
    }

    return next.handle(request).pipe(tap(() =>{
        handler();
      }));
  }
}
