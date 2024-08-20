import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';
import { CartService } from '../../../services/cart/cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userName: string | null = null;
  menuOpen = false;
  showLogoutButton = false;
  cartItemCount = 0;  // Contagem de itens do carrinho

  constructor(private authService: AuthService, private cartService: CartService) {}

  ngOnInit() {
    // Inscreve-se para obter o nome do usuário
    this.authService.userName$.subscribe(name => {
      this.userName = name;
    });

    // Inscreve-se para obter a contagem de itens do carrinho
    this.cartService.cartItemCount$.subscribe(count => {
      this.cartItemCount = count;
    });
  }

  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  logout() {
    this.authService.logout();
  }

  toggleLogoutButton(show: boolean) {
    this.showLogoutButton = show;
  }
}
