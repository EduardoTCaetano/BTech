import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AuthResponse } from '../../model/authresponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userEmailSubject = new BehaviorSubject<string | null>(null);
  userEmail$: Observable<string | null> = this.userEmailSubject.asObservable();

  private userNameSubject = new BehaviorSubject<string | null>(null);
  userName$: Observable<string | null> = this.userNameSubject.asObservable();

  private apiUrl = 'http://localhost:5246/api/User';

  constructor(private http: HttpClient) {
    this.loadUserFromLocalStorage();
  }

  private loadUserFromLocalStorage() {
    const token = localStorage.getItem('authToken');
    const email = localStorage.getItem('userEmail');
    const userName = localStorage.getItem('userName');

    if (token && email && userName) {
      this.userEmailSubject.next(email);
      this.userNameSubject.next(userName);
    }
  }

  login(email: string, password: string): Observable<AuthResponse> {
    const body = { EmailAddress: email, Password: password };
    return this.http.post<AuthResponse>(`${this.apiUrl}/Login`, body).pipe(
      tap((response) => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          localStorage.setItem('userEmail', response.emailAddress);
          localStorage.setItem('userName', response.userName);
          localStorage.setItem('userId', response.userId);
          this.userEmailSubject.next(response.emailAddress);
          this.userNameSubject.next(response.userName);
        }
      }),
      catchError((error) => {
        console.error('Login error', error);
        this.userEmailSubject.next(null);
        this.userNameSubject.next(null);
        // Retorna um valor padrão com todas as propriedades definidas como valores padrão
        return of({
          token: '', // String vazia como valor padrão
          emailAddress: '', // String vazia como valor padrão
          userName: '', // String vazia como valor padrão
          userId: '', // String vazia como valor padrão
          roles: [] // Array vazio como valor padrão
        } as AuthResponse);
      })
    );
  }

  logout() {
    localStorage.clear();
    this.userEmailSubject.next(null);
    this.userNameSubject.next(null);
  }

  getUserId(): Observable<string | null> {
    const userId = localStorage.getItem('userId');
    return of(userId); // Usa o operador 'of' para retornar um Observable de valor, ou nulo
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('authToken');
    return !!token;
  }
}
