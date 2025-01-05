import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { exhaustMap, take } from 'rxjs';
import { AuthenticationService } from './authentication.service';

export const authInterceptorService: HttpInterceptorFn = (req, next) => {
  let authenticationService: AuthenticationService = inject(
    AuthenticationService
  );

  return authenticationService.User.pipe(
    take(1),
    exhaustMap((user) => {
      if (user == null) {
        return next(req);
      }

      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${user.token}`),
      });
      return next(modifiedReq);
    })
  );
};
