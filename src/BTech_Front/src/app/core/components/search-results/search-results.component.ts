import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product/product.service';
import { ProductModel } from '../../../models/ProductModel';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})
export class SearchResultsComponent implements OnInit {
  searchResults: ProductModel[] = [];
  searchTerm: string = '';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit() {
    // Obter o termo da busca dos parâmetros da rota
    this.route.queryParams.subscribe((params) => {
      this.searchTerm = params['term'] || '';

      if (this.searchTerm) {
        // Chamar o serviço para buscar produtos
        this.productService.searchProducts(this.searchTerm).subscribe(
          (results) => {
            this.searchResults = results;
          },
          (error) => {
            console.error('Erro ao buscar produtos:', error);
          }
        );
      }
    });
  }
}
