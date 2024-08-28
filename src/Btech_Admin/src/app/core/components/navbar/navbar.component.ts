import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';  // Adjust the path as needed
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
  showLogoutButton = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.userName$.subscribe((name) => {
      this.userName = name;
      this.showLogoutButton = !!name;
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
        this.showLogoutButton = false;
      }
    });
  }
}
