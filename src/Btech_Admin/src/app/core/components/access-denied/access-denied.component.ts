import { Component } from '@angular/core';

@Component({
  selector: 'app-access-denied',
  template: `
    <h1>Acesso Negado</h1>
    <p>Você não tem permissão para acessar esta página.</p>
  `,
  styles: [`
    h1 {
      color: red;
    }
    p {
      font-size: 18px;
    }
  `]
})
export class AccessDeniedComponent { }
