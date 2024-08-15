import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router'; // Importe o Router
import { AuthService } from '../../../services/auth.service';
import { AuthResponse } from '../../../models/authresponsemodel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  passwordFieldType: string = 'password'; // For toggling password visibility
  errorMessage: string | null = null;
  isSignDivVisible: boolean = false; // Toggle between sign-in and sign-up forms

  constructor(private authService: AuthService, private router: Router) {} // Adicione o Router no construtor

  // Toggles the visibility of the password field
  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  // Handles user registration
  register(form: NgForm) {
    if (form.valid) {
      console.log('Registering:', this.email, this.password);
      this.authService.register(this.email, this.password).subscribe(
        (response: AuthResponse) => {
          console.log('Registration successful:', response);
          form.reset();
          this.errorMessage = null;
          // Handle successful registration (e.g., redirect or show a success message)
        },
        (error: any) => {
          console.error('Registration failed:', error);
          this.errorMessage = 'Registration failed. Please try again.';
        }
      );
    } else {
      this.errorMessage = 'Please fill out all required fields.';
    }
  }

  // Handles user login
  login(form: NgForm) {
    if (form.valid) {
      this.authService.login(this.email, this.password).subscribe(
        (response: AuthResponse) => {
          console.log('Login successful:', response);
          form.reset();
          this.errorMessage = null;
          // Redirect to home page
          this.router.navigate(['/home']); // Use o caminho da sua página inicial
        },
        (error: any) => {
          console.error('Login failed:', error);
          this.errorMessage = 'Login failed. Check your credentials and try again.';
        }
      );
    } else {
      this.errorMessage = 'Please fill out all required fields.';
    }
  }
}
