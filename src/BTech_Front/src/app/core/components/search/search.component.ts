import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CategoryService } from '../../../services/category/category.service';
import { CategoryModel } from '../../../models/categorymodel';
import { ProductModel } from '../../../models/ProductModel';
import { CartItem } from '../../../models/cartmodel';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { CartService } from '../../../services/cart/cart.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  products: ProductModel[] = [];
  filteredProducts: ProductModel[] = [];
  categories: CategoryModel[] = [];
  sidebarActive: boolean = false;
  selectedCategoryId: string | null = null;
  userId: string | undefined;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private router: Router,
    private authService: AuthService,
    private cartService: CartService,
  ) {}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(
      (data) => {
        this.products = data;
        this.filteredProducts = data;
      },
      (error) => {
        console.error('Erro ao carregar produtos', error);
      }
    );

    this.categoryService.getAllCategories().subscribe(
      (data) => {
        this.categories = data;
      },
      (error) => {
        console.error('Erro ao carregar categorias', error);
      }
    );
    this.authService.getUserId().subscribe(
      (id) => (this.userId = id),
      (error) => console.error('Erro ao obter userId', error)
    );
  }

  toggleSidebar(): void {
    this.sidebarActive = !this.sidebarActive;
  }

  selectCategory(category: CategoryModel): void {
    if (category && category.id) {
      this.selectedCategoryId = category.id;
      this.filterProducts();
    }
  }

  clearCategoryFilter(): void {
    this.selectedCategoryId = null;
    this.filterProducts();
  }

  filterProducts(): void {
    if (this.selectedCategoryId === null) {
      this.filteredProducts = this.products;
    } else {
      this.filteredProducts = this.products.filter(
        (product) => product.categoryId === this.selectedCategoryId
      );
    }
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
