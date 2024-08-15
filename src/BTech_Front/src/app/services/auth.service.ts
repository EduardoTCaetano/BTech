import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AuthResponse } from '../models/authresponsemodel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userEmailSubject = new BehaviorSubject<string | null>(null);
  userEmail$: Observable<string | null> = this.userEmailSubject.asObservable();

  private userNameSubject = new BehaviorSubject<string | null>(null);
  userName$: Observable<string | null> = this.userNameSubject.asObservable();

  private apiUrl = 'http://localhost:5246/api/User'; // Ajuste conforme suas rotas da API

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<AuthResponse> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = { EmailAddress: email, Password: password };
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, body, { headers }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          this.userEmailSubject.next(response.emailAddress);
          this.userNameSubject.next(response.userName);
        }
      }),
      catchError(error => {
        console.error('Login error', error);
        this.userEmailSubject.next(null);
        this.userNameSubject.next(null);
        throw error;
      })
    );
  }

  register(email: string, password: string): Observable<AuthResponse> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = { EmailAddress: email, Password: password };
    return this.http.post<AuthResponse>(`${this.apiUrl}/Register`, body, { headers }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          this.userEmailSubject.next(response.emailAddress);
          this.userNameSubject.next(response.userName);
        }
      }),
      catchError(error => {
        console.error('Registration error', error);
        this.userEmailSubject.next(null);
        this.userNameSubject.next(null);
        throw error;
      })
    );
  }

  logout() {
    localStorage.removeItem('authToken');
    this.userEmailSubject.next(null);
    this.userNameSubject.next(null);
  }
}
