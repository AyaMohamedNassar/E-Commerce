import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../Services/authentication.service';

export const canActivate = (
  router: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
):
  | boolean
  | UrlTree
  | Promise<boolean | UrlTree>
  | Observable<boolean | UrlTree> => {
  const authService = inject(AuthenticationService);
  const route = inject(Router);

  if (authService.loggedIn) {
    return true;
  } else {
    return route.createUrlTree(['/login']);
  }
};
