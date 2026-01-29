import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router) { }

  canActivate(

    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): any {

    var isAuthenticated = this.authService.isLoggedIn;
    if (!isAuthenticated) {
      this.router.navigate(["/account/login"]);
    }
    return isAuthenticated;

  }
}
