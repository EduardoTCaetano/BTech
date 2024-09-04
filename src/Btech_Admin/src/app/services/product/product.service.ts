import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductModel } from '../../model/product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'http://localhost:5246/api/Product';

  constructor(private http: HttpClient) {}

  private getAuthHeaders() {
    const token = localStorage.getItem('authToken');
    return {
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    };
  }

  createProduct(product: ProductModel): Observable<ProductModel> {
    return this.http.post<ProductModel>(this.apiUrl, product, {
      headers: this.getAuthHeaders(),
    });
  }

  getAllProducts(): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(this.apiUrl, {
      headers: this.getAuthHeaders(),
    });
  }

  getProductById(id: string): Observable<ProductModel> {
    return this.http.get<ProductModel>(`${this.apiUrl}/${id}`, {
      headers: this.getAuthHeaders(),
    });
  }

  updateProduct(id: string, product: ProductModel): Observable<ProductModel> {
    return this.http.put<ProductModel>(
      `${this.apiUrl}/${id}`,
      product,
      {
        headers: this.getAuthHeaders(),
      }
    );
  }

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, {
      headers: this.getAuthHeaders(),
    });
  }
}
