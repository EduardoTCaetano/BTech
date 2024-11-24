import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-purchase-list',
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.css']
})
export class PurchaseListComponent {

  constructor(private router: Router) {}

  navigateToSuccess(): void {
    this.router.navigate(['/underway']);
  }
}
