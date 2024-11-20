import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderPaymentService } from '../../../services/orderpayment/orderpayment.service';
import { Order } from '../../../models/order';

@Component({
  selector: 'app-profile-history',
  templateUrl: './profile-history.component.html',
  styleUrls: ['./profile-history.component.css']
})
export class ProfileHistoryComponent implements OnInit {
  orders: Order[] = [];
  userId: string | null = null;

  constructor(
    private router: Router,
    private orderPaymentService: OrderPaymentService
  ) {}

  ngOnInit(): void {
    this.getUserId();
    if (this.userId) {
      this.fetchOrders();
    }
  }

  getUserId(): void {
    // Recupera o ID do usuário armazenado no localStorage
    this.userId = localStorage.getItem('userId');
  }

  fetchOrders(): void {
    if (this.userId) {
      // Chama o serviço para buscar as ordens
      this.orderPaymentService.getOrdersByUserId(this.userId).subscribe(
        (orders: Order[]) => {
          this.orders = orders.map(order => ({
            ...order,
            productName: order.orderItems.map(item => item.nameProd).join(', '), // Concatenando os nomes dos produtos
            productImages: order.orderItems.slice(0, 2).map(item => item.image) // Pegando as duas primeiras imagens
          }));
          console.log('Pedidos carregados:', this.orders); // Exibe os dados no console
        },
        (error) => {
          console.error('Erro ao carregar pedidos:', error);
        }
      );
    }
  }



  profile() {
    this.router.navigate(['/sac']);
  }
}
