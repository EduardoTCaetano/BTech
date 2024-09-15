import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../../../services/cart/cart.service';
import { CartItem } from '../../../models/cartmodel';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { environment } from '../../../../enviroments/enviroments';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  cartItems: CartItem[] = [];
  subTotal: number = 0;
  payPalConfig?: IPayPalConfig;
  showSuccess = false;

  constructor(private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.loadCartItems();
    this.initPayPalConfig();
  }

  loadCartItems(): void {
    this.cartService.cartItems$.subscribe((items) => {
      this.cartItems = items;
      this.calculateSubTotal();
    });
  }

  calculateSubTotal(): void {
    this.subTotal = this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }

  private initPayPalConfig(): void {
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
                value: this.subTotal.toFixed(2),
                breakdown: {
                  item_total: {
                    currency_code: 'BRL',
                    value: this.subTotal.toFixed(2),
                  },
                },
              },
              items: this.cartItems.map(item => ({
                name: item.nameProd,
                quantity: item.quantity.toString(),
                category: 'PHYSICAL_GOODS',
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
        label: 'paypal',
        layout: 'vertical',
      },
      onApprove: (data, actions) => {
        console.log('onApprove - transaction was approved, but not authorized', data, actions);
        actions.order.get().then((details: any) => {
          console.log('onApprove - you can get full order details inside onApprove: ', details);
        });
      },
      onClientAuthorization: (data) => {
        console.log('onClientAuthorization - transaction completed', data);
        if (data.status === 'COMPLETED') {
          this.handlePaymentSuccess();
        }
      },
      onCancel: (data, actions) => {
        console.log('onCancel', data, actions);
      },
      onError: (err) => {
        console.log('onError', err);
      },
      onClick: (data, actions) => {
        console.log('onClick', data, actions);
      },
    };
  }

  private handlePaymentSuccess(): void {
    this.cartService.clearCart('user-id').subscribe(() => {
      this.router.navigate(['/success']);
    }, (error) => {
      console.error('Erro ao limpar o carrinho:', error);
    });
  }
}
