import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product/product.service';
import { CategoryService } from '../../../services/category/category.service';
import { CategoryModel } from '../../../models/categorymodel';
import { ProductModel } from '../../../models/ProductModel';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  products: ProductModel[] = [];
  filteredProducts: ProductModel[] = [];
  categories: CategoryModel[] = [];
  sidebarActive: boolean = false;
  selectedCategoryId: string | null = null;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(
      (data) => {
        this.products = data;
        this.filteredProducts = data;
      },
      (error) => {
        console.error('Erro ao carregar produtos', error);
      }
    );

    this.categoryService.getAllCategories().subscribe(
      (data) => {
        this.categories = data;
      },
      (error) => {
        console.error('Erro ao carregar categorias', error);
      }
    );
  }

  toggleSidebar(): void {
    this.sidebarActive = !this.sidebarActive;
  }

  selectCategory(category: CategoryModel): void {
    if (category && category.id) {
      this.selectedCategoryId = category.id;
      this.filterProducts();
    }
  }

  clearCategoryFilter(): void {
    this.selectedCategoryId = null;
    this.filterProducts();
  }

  filterProducts(): void {
    if (this.selectedCategoryId === null) {
      this.filteredProducts = this.products;
    } else {
      this.filteredProducts = this.products.filter(
        (product) => product.categoryId === this.selectedCategoryId
      );
    }
  }
}
