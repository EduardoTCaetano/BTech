import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] // Corrigido de 'styleUrl' para 'styleUrls'
})
export class AppComponent implements OnInit {
  emailAddress: string | null = null; // Alterado para 'emailAddress'
  title = 'BlitzTech_Front';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.authService.userEmail$.subscribe((email: string | null) => {
      this.emailAddress = email;
    });
  }
}
