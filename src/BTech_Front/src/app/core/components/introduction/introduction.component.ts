import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CartService } from '../../../services/cart/cart.service';
import { ProductModel } from '../../../models/ProductModel';
import { CartItem } from '../../../models/cartmodel';
import { AuthService } from '../../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-introduction',
  templateUrl: './introduction.component.html',
  styleUrls: ['./introduction.component.css'],
})
export class IntroductionComponent implements OnInit {
  products: ProductModel[] = [];
  userId: string | undefined;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const categoryId = 'a170fe18-5817-4b91-9085-556aafc68ccc';
    this.productService.getProductsByCategory(categoryId).subscribe(
      (data) => {
        this.products = data;
      },
      (error) => {
        console.error('Erro ao carregar produtos', error);
      }
    );

    this.authService.getUserId().subscribe(
      (id) => (this.userId = id),
      (error) => console.error('Erro ao obter userId', error)
    );
  }

  addToCart(event: Event, product: ProductModel): void {
    event.preventDefault(); 

    if (this.userId && product.id) {
      const cartItem: CartItem = {
        id: '',
        productId: product.id,
        quantity: 1,
        price: product.price,
        nameProd: product.name,
        userId: this.userId,
        image: product.image || '',
        total: product.price,
      };

      this.cartService.addCartItem(this.userId, cartItem).subscribe(
        (newItem) => {
          if (newItem) {
            console.log('Item adicionado ao carrinho', newItem);
            this.router.navigate(['/cart']);
          }
        },
        (error) => {
          console.error('Erro ao adicionar item ao carrinho', error);
        }
      );
    } else {
      console.error('Usuário não autenticado ou produto sem ID');
      this.router.navigate(['/login']);
    }
  }
}
