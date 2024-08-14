import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  isSignDivVisiable: boolean = true;
  passwordFieldType: string = 'password'; // Controla o tipo do campo de senha
  errorMessage: string | null = null; // Mensagem de erro

  // Alterna a visibilidade da senha
  togglePasswordVisibility() {
    this.passwordFieldType = this.passwordFieldType === 'password' ? 'text' : 'password';
  }

  // Submete o formulário e exibe mensagens de erro se necessário
  onSubmit(form: NgForm) {
    if (form.invalid) {
      this.errorMessage = this.getErrorMessage();
      return;
    }

    // Lógica de submissão do formulário
    console.log('Form Submitted', { email: this.email, password: this.password });

    // Limpa o formulário e a mensagem de erro após o envio
    form.reset();
    this.errorMessage = null;
  }

  // Obtém a mensagem de erro com base na validação dos campos
  private getErrorMessage(): string {
    const passwordPattern = /(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}/;
    if (!this.password) {
      return 'A senha é obrigatória.';
    } else if (this.password.length < 6) {
      return 'A senha deve ter pelo menos 6 caracteres.';
    } else if (!passwordPattern.test(this.password)) {
      return 'A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.';
    } else {
      return '';
    }
  }
}
  