import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AuthResponse } from '../../model/authresponse';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userEmailSubject = new BehaviorSubject<string | null>(null);
  userEmail$: Observable<string | null> = this.userEmailSubject.asObservable();

  private userNameSubject = new BehaviorSubject<string | null>(null);
  userName$: Observable<string | null> = this.userNameSubject.asObservable();

  private userRoleSubject = new BehaviorSubject<string | null>(null);
  userRole$: Observable<string | null> = this.userRoleSubject.asObservable();

  private apiUrl = 'http://localhost:5246/api/User';

  constructor(private http: HttpClient) {
    this.loadUserFromLocalStorage();
  }

  private loadUserFromLocalStorage() {
    const token = localStorage.getItem('authToken');
    const email = localStorage.getItem('userEmail');
    const userName = localStorage.getItem('userName');
    const userRole = localStorage.getItem('userRole');

    if (token && email && userName && userRole) {
      this.userEmailSubject.next(email);
      this.userNameSubject.next(userName);
      this.userRoleSubject.next(userRole);
    }
  }

  public saveToken(response: AuthResponse): void {
    const decodedToken = this.decodeToken(response.token);

    if (decodedToken) {
      console.log('Saving token with decoded data:', decodedToken);

      if (decodedToken.email && decodedToken.given_name) {
        localStorage.setItem('authToken', response.token);
        localStorage.setItem('userEmail', decodedToken.email);
        localStorage.setItem('userName', decodedToken.given_name);
        localStorage.setItem('userRole', decodedToken.role); // Ensure role is saved
        this.userEmailSubject.next(decodedToken.email);
        this.userNameSubject.next(decodedToken.given_name);
        this.userRoleSubject.next(decodedToken.role);
      } else {
        console.error('Token decodificado não possui as propriedades esperadas.', decodedToken);
        throw new Error('Falha ao salvar o token. Token decodificado é inválido.');
      }
    } else {
      console.error('Falha ao decodificar o token.');
      throw new Error('Token inválido.');
    }
  }

  login(email: string, password: string): Observable<AuthResponse> {
    const body = { EmailAddress: email, Password: password };
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, body).pipe(
      tap((response) => {
        console.log('Login response:', response);

        if (response && response.token) {
          const decodedToken = this.decodeToken(response.token);
          console.log('Decoded token:', decodedToken);

          if (decodedToken && decodedToken.role === 'Admin') {
            this.saveToken(response);
          } else {
            console.error('Acesso negado: Role não é Admin');
            Swal.fire({
              position: 'top-end',
              icon: 'error',
              title: 'Acesso Negado',
              text: 'Somente administradores podem acessar.',
              showConfirmButton: false,
              timer: 1500,
            });
            throw new Error('Acesso negado. Somente Administradores podem acessar.');
          }
        } else {
          console.error('Erro: Resposta de login inválida ou token ausente');
        }
      }),
      catchError((error) => {
        console.error('Erro de login', error);
        Swal.fire({
          position: 'top-end',
          icon: 'error',
          title: 'Erro de Login',
          text: 'Não foi possível realizar o login. Verifique suas credenciais.',
          showConfirmButton: false,
          timer: 1500,
        });
        this.userEmailSubject.next(null);
        this.userNameSubject.next(null);
        this.userRoleSubject.next(null);
        return of({
          token: '',
          emailAddress: '',
          userName: '',
          userId: '',
          roles: []
        } as AuthResponse);
      })
    );
  }

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      if (!payload) {
        throw new Error('Payload do token não encontrado.');
      }

      const decodedPayload = atob(payload);
      console.log('Payload decodificado:', decodedPayload);
      return JSON.parse(decodedPayload);
    } catch (e) {
      console.error('Erro ao decodificar o token:', e);
      return null;
    }
  }

  logout() {
    localStorage.clear();
    this.userEmailSubject.next(null);
    this.userNameSubject.next(null);
    this.userRoleSubject.next(null);
  }

  getUserId(): Observable<string | null> {
    const userId = localStorage.getItem('userId');
    return of(userId);
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('authToken');
    return !!token;
  }

  getUserRole(): string | null {
    return localStorage.getItem('userRole');
  }
}
