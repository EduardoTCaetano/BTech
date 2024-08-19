import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { AuthResponse } from '../../models/authresponsemodel';

@Injectable({
  providedIn: 'root'
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
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = { EmailAddress: email, Password: password };
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, body, { headers }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          localStorage.setItem('userEmail', response.emailAddress);
          localStorage.setItem('userId', response.userId);
          localStorage.setItem('userName', response.userName);
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
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, body, { headers }).pipe(
      tap(response => {
        if (response && response.token) {
          localStorage.setItem('authToken', response.token);
          localStorage.setItem('userEmail', response.emailAddress);
          localStorage.setItem('userName', response.userName);
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
    localStorage.removeItem('userEmail');
    localStorage.removeItem('userName');
    this.userEmailSubject.next(null);
    this.userNameSubject.next(null);
  }

  getUserId(): Observable<string> {
    const userId = localStorage.getItem('userId');
    if (userId) {
      return new Observable<string>(observer => {
        observer.next(userId);
        observer.complete();
      });
    } else {
      return new Observable<string>(observer => {
        observer.error('User ID not found');
      });
    }
  }
}
