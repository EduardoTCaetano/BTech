import { OrderItem } from "./orderitemmodel";

export interface Order {
  userId: string; // ou o tipo correspondente
  totalValue: number;
  orderItems: OrderItem[]; // Certifique-se de que OrderItem está definido corretamente
  merchantOrderId: string;
}
