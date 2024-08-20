import { Component, OnInit } from '@angular/core';
import { CartItem } from '../../../models/cartmodel';
import { AuthService } from '../../../services/auth/auth.service';
import { CartService } from '../../../services/cart/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  userId: string = '';
  subTotal: number = 0;

  constructor(
    private cartService: CartService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.getUserId().subscribe((userId) => {
      this.userId = userId;
      this.loadCartItems();
    });
  }

  loadCartItems(): void {
    this.cartService.getCartItems(this.userId).subscribe((items) => {
      this.cartItems = items;
      this.calculateSubTotal();
    });
  }

  removeItem(id: string): void {
    const userId = this.userId;
    console.log(`Removendo item com id: ${id} para o usuário: ${userId}`);

    this.cartService.removeCartItem(userId, id).subscribe(
      () => {
        console.log('Item removido com sucesso.');
        this.cartItems = this.cartItems.filter((item) => item.id !== id);
        this.calculateSubTotal();
      },
      (error) => {
        console.error('Erro ao remover o item:', error);
      }
    );
  }
  increaseQuantity(item: CartItem): void {
    item.quantity += 1;
    this.updateQuantity(item);
  }

  decreaseQuantity(item: CartItem): void {
    if (item.quantity > 1) {
      item.quantity -= 1;
      this.updateQuantity(item);
    }
  }

  updateQuantity(item: CartItem): void {
    const updatedItem: CartItem = { ...item };
    this.cartService.updateCartItem(updatedItem).subscribe((updated) => {
      const index = this.cartItems.findIndex((i) => i.id === item.id);
      if (index > -1) {
        this.cartItems[index] = updated;
        this.calculateSubTotal();
      }
    });
  }

  calculateSubTotal(): void {
    this.subTotal = this.cartItems.reduce(
      (total, item) => total + item.price * item.quantity,
      0
    );
  }
}
