import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userName: string | null = null;
  userRole: string | null = null;
  showLogoutButton = false;
  isPedidoUser = false;
  isMaster = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.userName$.subscribe((name) => {
      this.userName = name;
      this.showLogoutButton = !!name;
    });

    this.authService.userRole$.subscribe((role) => {
      this.userRole = role;
      this.isPedidoUser = role === 'Order';
      this.isMaster = role === 'Master';
    });
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
        this.userName = null;
        this.userRole = null;
        this.isPedidoUser = false;
        this.isMaster = false;
        this.router.navigate(['/login']);
      }
    });
  }
}
