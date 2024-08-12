import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ProductModel } from '../../../models/ProductModel';

@Component({
  selector: 'app-introduction',
  templateUrl: './introduction.component.html',
  styleUrls: ['./introduction.component.css']
})
export class IntroductionComponent implements OnInit {

  products: ProductModel[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    const categoryId = 'a170fe18-5817-4b91-9085-556aafc68ccc';
    this.productService.getProductsByCategory(categoryId).subscribe(
      (data) => {
        this.products = data;
      },
      (error) => {
        console.error('Erro ao carregar produtos', error);
      }
    );
  }
}
