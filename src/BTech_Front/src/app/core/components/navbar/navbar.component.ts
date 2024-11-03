import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';
import { CartService } from '../../../services/cart/cart.service';
import { ProductService } from '../../../services/product/product.service';
import { CartItem } from '../../../models/cartmodel';
import { ProductModel } from '../../../models/ProductModel';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userName: string | null = null;
  menuOpen = false;
  showLogoutButton = false;
  cartItemCount = 0;
  cartItems: CartItem[] = [];
  userId: string = '';
  searchTerm: string = ''; // Campo para armazenar o termo de busca
  searchResults: ProductModel[] = []; // Array para armazenar resultados da busca

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.userName$.subscribe((name) => {
      this.userName = name;
    });

    this.authService.getUserId().subscribe((userId) => {
      this.userId = userId;
      this.loadCartItems();
    });

    this.cartService.cartItemCount$.subscribe((count) => {
      this.cartItemCount = count;
    });
  }

  loadCartItems(): void {
    this.cartService.getCartItems(this.userId).subscribe((items) => {
      this.cartItems = items;
      this.cartItemCount = items.length;
    });
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  logout() {
    Swal.fire({
      title: 'Tem certeza?',
      text: 'Você deseja realmente sair?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sim, sair',
      cancelButtonText: 'Cancelar',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.logout();
        this.router.navigate(['/login']);
      }
    });
  }

  toggleLogoutButton(show: boolean) {
    this.showLogoutButton = show;
  }

  searchProducts() {
    if (this.searchTerm.trim()) {
      this.router.navigate(['/search'], { queryParams: { term: this.searchTerm } });
    }
  }
}
