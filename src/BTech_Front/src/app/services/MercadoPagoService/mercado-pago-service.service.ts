import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroments/enviroments';


@Injectable({
  providedIn: 'root'
})
export class MercadoPagoServiceService {
  private accessToken = environment.mercadoPagoAccessToken;

  constructor(private http: HttpClient) {}

  createPreference(preferenceData: any) {
    return this.http.post('https://api.mercadopago.com/checkout/preferences', preferenceData, {
      headers: {
        Authorization: `Bearer ${this.accessToken}`
      }
    });
  }
}
