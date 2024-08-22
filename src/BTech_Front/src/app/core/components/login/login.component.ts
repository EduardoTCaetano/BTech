import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { AuthResponse } from '../../../models/authresponsemodel';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  UserName: string = '';
  email: string = '';
  password: string = '';
  passwordFieldType: string = 'password';
  errorMessage: string | null = null;
  isSignDivVisible: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  togglePasswordVisibility() {
    this.passwordFieldType =
      this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  register(form: NgForm) {
    if (form.valid) {
      this.authService.register(this.UserName, this.email, this.password).subscribe(
        (response: AuthResponse) => {
          form.reset();
          this.errorMessage = null;
          Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Registro bem-sucedido!',
            text: 'Você foi registrado com sucesso.',
            showConfirmButton: false,
            timer: 1500,
          }).then(() => {
            window.location.reload();
          });
        },
        (error: any) => {
          this.errorMessage = 'Registro falhou. Tente novamente.';
          Swal.fire({
            position: 'top-end',
            icon: 'error',
            title: 'Falha no Registro',
            text: 'Não foi possível registrar. Tente novamente.',
            showConfirmButton: false,
            timer: 1500,
          });
        }
      );
    } else {
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios.';
    }
  }

  login(form: NgForm) {
    if (form.valid) {
      this.authService.login(this.email, this.password).subscribe(
        (response: AuthResponse) => {
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
        },
        (error: any) => {
          this.errorMessage =
            'Falha no login. Verifique suas credenciais e tente novamente.';
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
