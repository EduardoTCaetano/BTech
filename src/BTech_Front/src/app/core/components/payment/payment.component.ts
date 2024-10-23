import { Component, OnInit } from '@angular/core';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { CartService } from '../../../services/cart/cart.service';
import { AuthService } from '../../../services/auth/auth.service';
import { OrderService } from '../../../services/order/order.service';
import { environment } from '../../../../enviroments/enviroments';
import { Router } from '@angular/router';

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

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private orderService: OrderService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initConfig();
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

  private initConfig(): void {
    this.payPalConfig = {
      currency: 'BRL',
      clientId: environment.paypalClientId,
      createOrderOnClient: (data) =>
        <ICreateOrderRequest>{
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
          console.log('Order approved, transaction details:', details);
          this.createOrder();
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

  private createOrder(): void {
    const orderItems = this.cartItems.map((item) => ({
      productId: item.productId,
      quantity: item.quantity,
      unitPrice: item.price,
    }));

    this.authService.getUserId().subscribe((userId) => {
      const order = {
        userId,
        totalValue: this.totalValue,
        orderItems,
      };

      this.orderService.createOrder(order).subscribe(() => {
        this.cartService.clearCart(userId).subscribe(() => {
          console.log('Carrinho limpo após pagamento');
          this.cartItems = [];
          this.totalValue = 0;
          this.router.navigate(['/success']);
        });
      });
    });
  }
}
