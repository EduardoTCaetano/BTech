import { OrderItem } from "./orderitemmodel";

export interface Order {
  id?: string;
  userId: string;
  items: OrderItem[];
  totalAmount: number;
  createdAt?: Date;
}
