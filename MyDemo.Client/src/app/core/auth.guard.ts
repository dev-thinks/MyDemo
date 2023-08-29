import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanActivateChild,
  RoutesRecognized,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { filter, pairwise } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {

  previousUrl!: string;
  currentUrl!: string;

  constructor(private auth: AuthService, private router: Router) {
    this.router.events
      .pipe(filter((evt: any) => evt instanceof RoutesRecognized), pairwise())
      .subscribe((events: RoutesRecognized[]) => {
        this.previousUrl = events[0].urlAfterRedirects;
        this.currentUrl = events[1].urlAfterRedirects;
      });
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.authenticate();
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree {
    return this.authenticate();
  }

  private authenticate(): boolean | UrlTree {
    //check whether is login successfully
    if (this.auth.check()) {
      return true;
    }
    else{
      this.router.navigateByUrl('/login');
    }
    return false;
  }
}
