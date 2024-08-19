import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CartItem } from '../../models/cartmodel';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = 'http://localhost:5246/api/Cart';

  constructor(private http: HttpClient) {}

  getCartItems(userId: string): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.apiUrl}/${userId}`);
  }

  removeCartItem(userId: string, id: string): Observable<void> {
    const url = `${this.apiUrl}/user/${userId}/item/${id}`;
    console.log(`URL da solicitação DELETE: ${url}`);
    return this.http.delete<void>(url);
  }

  updateCartItem(cartItem: CartItem): Observable<CartItem> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<CartItem>(
      `${this.apiUrl}/items/${cartItem.id}`,
      cartItem,
      { headers }
    );
  }
}
