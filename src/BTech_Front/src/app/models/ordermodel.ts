import { OrderItem } from "./orderitemmodel";

export interface Order {
  userId: string;
  totalValue: number;
  orderItems: OrderItem[];
  merchantOrderId: string;
}


