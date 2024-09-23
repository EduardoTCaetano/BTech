import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
import { Observable, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.isLoggedIn()) {
      const expectedRole = route.data['role'];
      if (expectedRole) {
        return this.authService.userRole$.pipe(
          map(userRole => {
            if (userRole === 'Master' || userRole === expectedRole) {
              return true;
            } else {
              this.router.navigate(['/access-denied']);
              return false;
            }
          }),
          tap(isAuthorized => {
            if (!isAuthorized) {
            }
          })
        );
      }
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
