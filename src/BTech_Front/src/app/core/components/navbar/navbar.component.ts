import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent {
  menuOpen = false;

  toggleMenu() {
      this.menuOpen = !this.menuOpen;
  }

}
