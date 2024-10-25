export interface ProductModel {
  id?: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  image: string;
  isActive: boolean;
  categoryId: string;
}
