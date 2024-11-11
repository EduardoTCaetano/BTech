import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../../models/ordermodel';

@Injectable({
  providedIn: 'root'
})
export class OrderPaymentService {
  private readonly apiUrl = 'http://localhost:5246/api/Order';

  constructor(private http: HttpClient) {}

  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, order);
  }
}
