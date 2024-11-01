interface Window {
  MercadoPago: any;
}


export interface Order {
  userId: string; // ou o tipo correspondente
  totalValue: number;
  orderItems: Array<{
    productId: string; // ou o tipo correspondente
    quantity: number;
    unitPrice: number;
  }>;
  merchantOrderId: string;
}

