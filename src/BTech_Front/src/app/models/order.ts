export interface Order {
  id: string;
  name: string;
  productName: string;
  productImages?: string[]; // Adicionando a propriedade productImages
  date: string;
  status: string;
  userId: string;
  deliveryDate: Date;
  totalValue: number;
  orderItems: any[];
  merchantOrderId: string;
}
