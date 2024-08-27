import { NgClass } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms'; // Import FormsModule for template-driven forms

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgClass], // Import FormsModule instead of NgForm
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  passwordFieldType: string = 'password';
  errorMessage: string | null = null;

  togglePasswordVisibility(): void {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  login(form: NgForm): void { // Use NgForm type for the form parameter
    if (form.valid) {
      // Handle login logic here
      console.log('Login successful');
    } else {
      this.errorMessage = 'Please fill in all required fields.';
    }
  }
}
