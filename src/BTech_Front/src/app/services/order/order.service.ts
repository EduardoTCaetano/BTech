import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../enviroments/enviroments';
import { Order } from '../../models/ordermodel';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'https://api.mercadopago.com/merchant_orders';

  constructor(private http: HttpClient) {}

  getMerchantOrder(orderId: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${orderId}`, {
      headers: {
        Authorization: `Bearer ${environment.mercadoPagoAccessToken}`,
      },
    });
  }

  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}`, order, {
      headers: {
        Authorization: `Bearer ${environment.mercadoPagoAccessToken}`,
        'Content-Type': 'application/json'
      }
    });
  }
}
