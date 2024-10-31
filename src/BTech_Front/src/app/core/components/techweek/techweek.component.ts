import { Component, ElementRef, ViewChild } from '@angular/core';
import { ProductModel } from '../../../models/ProductModel';
import { Router } from '@angular/router';
import { CartItem } from '../../../models/cartmodel';
import { AuthService } from '../../../services/auth/auth.service';
import { CartService } from '../../../services/cart/cart.service';
import { ProductService } from '../../../services/product/product.service';

@Component({
  selector: 'app-techweek',
  templateUrl: './techweek.component.html',
  styleUrls: ['./techweek.component.css']
})
export class TechweekComponent {
  @ViewChild('containerRef') containerRef!: ElementRef;

  products: ProductModel[] = [];
  userId: string | undefined;
  private isDragging = false;
  private startX = 0;
  private scrollLeft = 0;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const categoryId = '218e1211-760b-4449-9839-7e14fc94cd4b';
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

  onDragStart(event: MouseEvent): void {
    this.isDragging = true;
    this.startX = event.pageX - this.containerRef.nativeElement.offsetLeft;
    this.scrollLeft = this.containerRef.nativeElement.scrollLeft;
  }

  onDragging(event: MouseEvent): void {
    if (!this.isDragging) return;
    event.preventDefault();
    const x = event.pageX - this.containerRef.nativeElement.offsetLeft;
    const walk = (x - this.startX) * 2; // Ajuste a velocidade do scroll multiplicando
    this.containerRef.nativeElement.scrollLeft = this.scrollLeft - walk;
  }

  onDragEnd(): void {
    this.isDragging = false;
  }
}
