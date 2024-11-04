import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-history',
  templateUrl: './profile-history.component.html',
  styleUrls: ['./profile-history.component.css']  // Corrected property name
})
export class ProfileHistoryComponent {
  constructor(

    private router: Router
  ) {}

  profile() {
    this.router.navigate(['/sac']);
  }
}
