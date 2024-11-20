import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  userName: string | null = null;
  userEmail: string | null = null;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.userName$.subscribe((name) => {
      this.userName = name;
    });

    this.authService.userEmail$.subscribe((email) => {
      this.userEmail = email;
    });
  }
}
