import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { CartItem } from '../../models/cartmodel';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = 'http://localhost:5246/api/Cart';

  private cartItemsSource = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItemsSource.asObservable();

  private cartItemCountSource = new BehaviorSubject<number>(0);
  cartItemCount$ = this.cartItemCountSource.asObservable();

  constructor(private http: HttpClient) {
    this.cartItems$.subscribe(items => this.cartItemCountSource.next(items.length));
  }

  private isAuthenticated(): boolean {
    return !!localStorage.getItem('authToken');
  }

  private getAuthHeaders() {
    const token = localStorage.getItem('authToken');
    return {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    };
  }

  getCartItems(userId: string): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/${userId}`).pipe(
      tap((items) => {
        this.cartItemsSource.next(items);
        this.cartItemCountSource.next(items.length);
      }),
      catchError((error) => {
        console.error('Erro ao obter itens do carrinho', error);
        return of([]);
      })
    );
  }

  removeCartItem(userId: string, id: string): Observable<void> {
    const url = `${this.apiUrl}/user/${userId}/item/${id}`;
    return this.http.delete<void>(url).pipe(
      tap(() => {
        const currentItems = this.cartItemsSource.value.filter((item) => item.id !== id);
        this.cartItemsSource.next(currentItems);
        this.cartItemCountSource.next(currentItems.length);
      }),
      catchError((error) => {
        console.error('Erro ao remover item do carrinho', error);
        return of(void 0);
      })
    );
  }

  updateCartItem(cartItem: CartItem): Observable<CartItem> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http
      .put<CartItem>(`${this.apiUrl}/item/${cartItem.id}`, cartItem, { headers })
      .pipe(
        tap((updatedItem) => {
          const currentItems = this.cartItemsSource.value.map((item) =>
            item.id === updatedItem.id ? updatedItem : item
          );
          this.cartItemsSource.next(currentItems);
          this.cartItemCountSource.next(currentItems.length);
        }),
        catchError((error) => {
          console.error('Erro ao atualizar item no carrinho', error);
          return of(cartItem);
        })
      );
  }

  addCartItem(userId: string, cartItem: CartItem): Observable<CartItem> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<CartItem>(`${this.apiUrl}/user/${userId}/items`, cartItem, { headers }).pipe(
      tap((response) => {
        const updatedItems = [...this.cartItemsSource.value, response];
        this.cartItemsSource.next(updatedItems);
        this.cartItemCountSource.next(updatedItems.length);
      }),
      catchError((error) => {
        console.error('Erro na requisição:', error.message || error);
        return throwError(() => new Error('Falha ao adicionar item ao carrinho'));
      })
    );
  }

  clearCart(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/user/${userId}`, {
      headers: this.getAuthHeaders(),
    }).pipe(
      tap(() => {
        this.cartItemsSource.next([]);
        this.cartItemCountSource.next(0);
      }),
      catchError((error) => {
        console.error('Erro ao limpar carrinho', error);
        return of(void 0);
      })
    );
  }
}
