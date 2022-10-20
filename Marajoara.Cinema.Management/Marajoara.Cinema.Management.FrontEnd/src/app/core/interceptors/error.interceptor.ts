
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ToastrService } from 'src/app/services/toastr.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthenticationService,
        private toastrService: ToastrService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError((err,caught) => {
            var error: string;
            if ([401, 403].includes(err.status) && this.authService.authorizedUserAccount) {
                // auto logout if 401 or 403 response returned from api
                this.authService.logout();
                error = 'Sessão expirada!';
            } else {
                error = err.error?.message || err.statusText;
            }
            this.toastrService.showErrorMessage(error);
            
            //console.error(err);
            return throwError(() => new Error(error));
        }));
    }
}