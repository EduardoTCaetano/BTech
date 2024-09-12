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
    // Verifica se o usuário está logado
    if (this.authService.isLoggedIn()) {
      // Obtém o papel esperado da rota
      const expectedRole = route.data['role'];
      if (expectedRole) {
        // Obtém o papel do usuário e verifica se corresponde ao esperado
        return this.authService.userRole$.pipe(
          map(userRole => {
            if (userRole === expectedRole) {
              return true;
            } else {
              this.router.navigate(['/access-denied']); // Roteia para uma página de acesso negado
              return false;
            }
          }),
          tap(isAuthorized => {
            if (!isAuthorized) {
              // Pode adicionar lógica adicional para lidar com não autorização
            }
          })
        );
      }
      return true; // Permite acesso se não houver papel esperado
    } else {
      this.router.navigate(['/login']); // Roteia para a página de login se não estiver logado
      return false;
    }
  }
}
