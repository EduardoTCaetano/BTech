import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../../services/auth/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AuthResponse } from '../../../model/authresponse';
import { CommonModule } from '@angular/common';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  passwordFieldType: string = 'password';
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  togglePasswordVisibility() {
    this.passwordFieldType =
      this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  login(form: NgForm) {
    if (form.valid) {
      this.authService.login(this.email, this.password).subscribe(
        (response: AuthResponse) => {
          // Get the user role from the AuthService
          this.authService.userRole$.subscribe((userRole) => {
            if (userRole === 'Admin' || userRole === 'Order') {
              form.reset();
              this.errorMessage = null;
              Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Login bem-sucedido!',
                text: 'Você está agora logado.',
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                this.router.navigate(['/home']);
              });
            } else {
              this.errorMessage = 'Acesso negado. Apenas administradores ou responsáveis por pedidos podem logar.';
              Swal.fire({
                position: 'top-end',
                icon: 'error',
                title: 'Acesso Negado',
                text: 'Apenas administradores ou responsáveis por pedidos podem logar.',
                showConfirmButton: false,
                timer: 1500,
              });
            }
          });
        },
        (error: any) => {
          this.errorMessage = 'Falha no login. Verifique suas credenciais e tente novamente.';
          Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Falha no Login',
            text: 'Não foi possível fazer login. Verifique suas credenciais.',
            showConfirmButton: false,
            timer: 1500,
          });
        }
      );
    } else {
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios.';
    }
  }
}
