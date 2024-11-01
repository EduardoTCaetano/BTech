import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../../services/order/order.service';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent implements OnInit {
  merchantOrderId: string | null = null;
  orderStatus: any;
  orderItems: any[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      this.merchantOrderId = params.get('merchant_order_id');
      console.log('Chamando API com merchantOrderId:', this.merchantOrderId);
      if (this.merchantOrderId) {
        this.getOrderStatus(this.merchantOrderId);
      }
    });
  }

  getOrderStatus(merchantOrderId: string): void {
    console.log(`Iniciando a chamada da API para o ID do pedido: ${merchantOrderId}`);

    this.orderService.getMerchantOrder(merchantOrderId).subscribe(
      (response: any) => {
        console.log('Resposta da API:', response);
        this.orderStatus = response;

        if (response.items && response.items.length > 0) {
          this.orderItems = response.items;
          console.log('Itens do pedido:', this.orderItems);
        } else {
          console.warn('Nenhum item encontrado na resposta:', response);
          this.orderItems = [];
        }
      },
      (error) => {
        console.error('Erro ao obter status do pedido no Mercado Pago:', error);
        alert('Erro ao obter informações do pedido. Verifique os detalhes no console.');
      }
    );
  }


  goHome(): void {
    this.router.navigate(['/']);
  }
}
