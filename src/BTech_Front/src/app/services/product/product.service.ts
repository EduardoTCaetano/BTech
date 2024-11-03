import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductModel } from '../../models/ProductModel';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  private apiUrl = 'http://localhost:5246/api/Product';

  constructor(private http: HttpClient) {}

  getAllProducts(): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(this.apiUrl);
  }

  getProductById(id: string): Observable<ProductModel> {
    return this.http.get<ProductModel>(`${this.apiUrl}/${id}`);
  }

  getProductsByCategory(categoryId: string): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(
      `${this.apiUrl}/category/${categoryId}`
    );
  }

  requestApproval(product: ProductModel): Observable<any> {
    return this.http.post('/aprovacao', product);
  }

  searchProducts(name: string): Observable<ProductModel[]> {
    return this.http.get<ProductModel[]>(`${this.apiUrl}/search?name=${name}`);
}


}
