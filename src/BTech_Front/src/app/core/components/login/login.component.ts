import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth/auth.service';
import { AuthResponse } from '../../../models/authresponsemodel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
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
      this.authService.register(this.email, this.password).subscribe(
        (response: AuthResponse) => {
          form.reset();
          this.errorMessage = null;
        },
        (error: any) => {
          this.errorMessage = 'Registration failed. Please try again.';
        }
      );
    } else {
      this.errorMessage = 'Please fill out all required fields.';
    }
  }

  login(form: NgForm) {
    if (form.valid) {
      this.authService.login(this.email, this.password).subscribe(
        (response: AuthResponse) => {
          form.reset();
          this.errorMessage = null;
          this.router.navigate(['/home']);
        },
        (error: any) => {
          this.errorMessage =
            'Login failed. Check your credentials and try again.';
        }
      );
    } else {
      this.errorMessage = 'Please fill out all required fields.';
    }
  }
}
