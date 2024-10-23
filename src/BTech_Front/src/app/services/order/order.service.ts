import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, catchError, tap, of } from 'rxjs';
import { Order } from '../../models/ordermodel';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'http://localhost:5246/api/Order';

  constructor(private http: HttpClient) {}

  createOrder(order: Order): Observable<Order | null> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<Order>(`${this.apiUrl}/CreateOrder`, order, { headers }).pipe(
      tap((newOrder) => {
        console.log('Pedido criado:', newOrder);
      }),
      catchError((error) => {
        console.error('Erro ao criar pedido', error);
        return of(null);
      })
    );
  }

  getOrders(userId: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/GetOrders/${userId}`).pipe(
      tap((orders) => {
        console.log('Pedidos obtidos:', orders);
      }),
      catchError((error) => {
        console.error('Erro ao obter pedidos', error);
        return of([]);
      })
    );
  }
}
