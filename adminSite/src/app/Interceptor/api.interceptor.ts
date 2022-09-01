import { Injectable } from '@angular/core';
import {
    HttpInterceptor,
    HttpRequest,
    HttpResponse,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse
} from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable() export class ApiInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token: any = sessionStorage.getItem('token');

        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }
        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }
        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });
        console.log(request);
        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    console.log('event--->>>', event);
                }
                return event;
            }), catchError((error: HttpErrorResponse) => {
                if (error.status == 401) {
                    sessionStorage.removeItem("token");
                }
                let data = {};
                data = {
                    reason: error && error.error.reason ? error.error.reason : '',
                    status: error.status
                };
                console.log("error handled")
                return throwError(() => error);
            })
        );
    }
}