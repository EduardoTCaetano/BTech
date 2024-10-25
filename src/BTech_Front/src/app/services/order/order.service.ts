import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Order } from '../../models/ordermodel';
import { OrderItem } from '../../models/orderitemmodel';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'http://localhost:5246/api/Order';

  constructor(private http: HttpClient) {}

  createOrder(userId: string, orderItems: OrderItem[]): Observable<Order | null> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const orderData = {
      userId,
      items: orderItems,
      totalAmount: orderItems.reduce((sum, item) => sum + item.unitPrice * item.quantity, 0),
    };

    return this.http.post<Order>(`${this.apiUrl}/CreateOrder`, orderData, { headers }).pipe(
      tap((newOrder) => console.log('Order created:', newOrder)),
      catchError((error) => {
        console.error('Error creating order', error);
        return of(null);
      })
    );
  }
}
