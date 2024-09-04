import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product/product.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css'],
})
export class ProductPageComponent implements OnInit {
  product: any;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('id');
    if (productId) {
      this.productService.getProductById(productId).subscribe(data => {
        this.product = data;

        if (!this.product.imageUrl) {
          this.product.imageUrl = 'https://exemplo.com/imagem-da-pagina-home.jpg';
        }

        console.log('Image URL:', this.product.imageUrl);
      });
    }
  }

  activateCredit() {
    console.log('Credit activated!');
  }
}
