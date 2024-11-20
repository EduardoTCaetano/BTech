import { Component, OnInit } from '@angular/core';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { CartService } from '../../../services/cart/cart.service';
import { AuthService } from '../../../services/auth/auth.service';
import { OrderService } from '../../../services/order/order.service';
import { OrderPaymentService } from '../../../services/orderpayment/orderpayment.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../enviroments/enviroments';
import { Order } from '../../../models/order';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  public payPalConfig?: IPayPalConfig;
  public totalValue: number = 0;
  public cartItems: any[] = [];
  public subTotal: number = 0;
  merchantOrderId: string | undefined;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private orderService: OrderService,
    private orderPaymentService: OrderPaymentService,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.initPayPalConfig();
    this.loadCartItems();
  }

  private loadCartItems(): void {
    this.authService.getUserId().subscribe((userId) => {
      this.cartService.getCartItems(userId).subscribe((items) => {
        this.cartItems = items;
        this.totalValue = this.cartItems.reduce(
          (acc, item) => acc + item.price * item.quantity,
          0
        );
        this.subTotal = this.totalValue;
      });
    });
  }

  private initPayPalConfig(): void {
    this.payPalConfig = {
      currency: 'BRL',
      clientId: environment.paypalClientId,
      createOrderOnClient: () =>
        <ICreateOrderRequest> {
          intent: 'CAPTURE',
          purchase_units: [
            {
              amount: {
                currency_code: 'BRL',
                value: this.totalValue.toFixed(2),
                breakdown: {
                  item_total: {
                    currency_code: 'BRL',
                    value: this.totalValue.toFixed(2),
                  },
                },
              },
              items: this.cartItems.map((item) => ({
                name: item.nameProd,
                quantity: item.quantity.toString(),
                unit_amount: {
                  currency_code: 'BRL',
                  value: item.price.toFixed(2),
                },
              })),
            },
          ],
        },
      advanced: {
        commit: 'true',
      },
      style: {
        layout: 'vertical',
      },
      onApprove: (data, actions) => {
        actions.order.get().then((details: any) => {
          this.createOrder(details.id);
          this.router.navigate(['/sucesso']);
        });
      },
      onClientAuthorization: (data) => {
        console.log('Payment authorized:', data);
      },
      onError: (err) => {
        console.error('Error during PayPal payment:', err);
      },
      onCancel: (data, actions) => {
        console.log('Payment cancelled:', data, actions);
      },
    };
  }

  private initMercadoPagoConfig(): void {
    const preference = {
      items: this.cartItems.map((item) => ({
        title: item.nameProd,
        quantity: item.quantity,
        currency_id: 'BRL',
        unit_price: item.price,
      })),
      payer: {
        email: 'gabrielhenriquesantanagg@gmail.com'
      },
      back_urls: {
        success: `${window.location.origin}/success`,
        failure: `${window.location.origin}/failure`,
        pending: `${window.location.origin}/pending`
      },
      auto_return: 'approved',
      payment_methods: {
        excluded_payment_types: [],
        included_payment_methods: [{ id: 'pix' }]
      }
    };

    this.http.post('https://api.mercadopago.com/checkout/preferences', preference, {
      headers: {
        Authorization: `Bearer ${environment.mercadoPagoAccessToken}`
      }
    }).subscribe((response: any) => {
      this.merchantOrderId = response.id;
      window.location.href = response.init_point;
    }, (error) => {
      console.error('Erro ao criar preferência no Mercado Pago:', error);
    });
  }

  private createOrder(paymentId: string): void {
    const orderItems = this.cartItems.map((item) => ({
      productId: item.productId,
      nameProd: item.nameProd,
      image: item.image,
      quantity: item.quantity,
      price: item.price,
    }));

    this.authService.getUserId().subscribe((userId) => {
      const productImage = this.cartItems[0]?.image || '';

      const order: Order = {
        id: paymentId,
        name: `Pedido ${paymentId}`,
        productName: this.cartItems.map(item => item.nameProd).join(', '),
        productImages: productImage,
        date: new Date().toISOString(),
        status: 'pending',
        userId,
        deliveryDate: new Date(),
        totalValue: this.totalValue,
        orderItems,
        merchantOrderId: this.merchantOrderId || '',
      };

      this.orderPaymentService.createOrder(order).subscribe(
        (response: Order) => {
          if (response && response.merchantOrderId) {
            this.merchantOrderId = response.merchantOrderId;
            console.log('ID do pedido gerado:', response.merchantOrderId);
            this.clearCartAfterPurchase(userId);
            this.router.navigate(['/sucesso']);
          }
        },
        (error) => {
          console.error('Erro ao criar pedido:', error);
        }
      );
    });
  }

  private clearCartAfterPurchase(userId: string): void {
    this.cartService.clearCart(userId).subscribe(() => {
      this.cartItems = [];
      this.totalValue = 0;
      this.subTotal = 0;
      this.router.navigate(['/home', this.merchantOrderId]);
    });
  }

  public initiatePayment(paymentMethod: 'paypal' | 'mercadoPago'): void {
    if (paymentMethod === 'paypal') {
      this.initPayPalConfig();
    } else if (paymentMethod === 'mercadoPago') {
      this.initMercadoPagoConfig();
    }
  }
}
