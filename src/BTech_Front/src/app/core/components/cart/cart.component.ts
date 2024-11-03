import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart/cart.service';
import { AuthService } from '../../../services/auth/auth.service';
import { CartItem } from '../../../models/cartmodel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  totalValue: number = 0;
  subTotal: number = 0;
  userId: string | null = null;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.getUserId().subscribe({
      next: (userId) => {
        this.userId = userId;
        this.loadCartItems(userId);
      },
      error: () => this.router.navigate(['/login']),
    });
  }

  private loadCartItems(userId: string): void {
    this.cartService.getCartItems(userId).subscribe((items) => {
      this.cartItems = items;
      this.calculateTotal();
    });
  }

  private calculateTotal(): void {
    this.subTotal = this.cartItems.reduce((sum, item) => sum + item.total, 0);
    this.totalValue = this.subTotal;
  }

  public removeItem(itemId: string): void {
    if (this.userId) {
      this.cartService.removeCartItem(this.userId, itemId).subscribe(() => {
        this.cartItems = this.cartItems.filter(item => item.id !== itemId);
        this.calculateTotal();
      });
    }
  }

  public clearCart(): void {
    if (this.userId) {
      this.cartService.clearCart(this.userId).subscribe(() => {
        this.cartItems = [];
        this.subTotal = 0;
        this.totalValue = 0;
      });
    }
  }

  public proceedToPayment(): void {
    if (this.cartItems.length === 0) {
      alert('Carrinho está vazio!');
    } else {
      this.router.navigate(['/payment']);
    }
  }

  public increaseQuantity(item: CartItem): void {
    item.quantity += 1;
    item.total = item.price * item.quantity;
    this.calculateTotal();
  }

  public decreaseQuantity(item: CartItem): void {
    if (item.quantity > 1) {
      item.quantity -= 1;
      item.total = item.price * item.quantity;
      this.calculateTotal();
    }
  }

  public finalizePurchase(): void {
    if (this.cartItems.length === 0) {
      alert('Carrinho está vazio!');
      return;
    }
    this.router.navigate(['/payment']);
  }
}
