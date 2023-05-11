import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { Injectable } from '@angular/core';
import { tap, catchError } from 'rxjs/operators';
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {

  public static methodsToProcess = ['GET', 'POST', 'PUT', 'DELETE'];
  public static showToastMomentaneously = true;
  public static successfulResponses = [201, 200];
  public static errorResponses = [400, 401, 403, 404, 405];
  public static serverErrorResponses = [500, 504];

  constructor(public snackBar: MatSnackBar) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const idToken = localStorage.getItem('token');

    if (idToken) {
      req = req.clone({
        headers: req.headers.set('Authorization',
          'Bearer ' + idToken)
      });
    }

    return next.handle(req).pipe(tap(
      evt => {
        if (evt instanceof HttpResponse) {
        }
      }), catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status == 401 && !err.error)
            this.snackBar.open('Sesija je istekla. Ulogujte se ponovo.', 'OK', {duration: 2500});
        }
        return throwError(err);
      }));
  }
}