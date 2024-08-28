import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductModel } from '../../../model/product';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductService } from '../../../services/product/product.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  products: ProductModel[] = [];
  productForm: FormGroup;

  constructor(
    private productService: ProductService,
    private fb: FormBuilder
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      image: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe((data: ProductModel[]) => {
      this.products = data;
    });
  }

  openModal(): void {
    const modal = document.getElementById('addProductModal');
    if (modal) {
      (window as any).$(modal).modal('show');
    }
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const newProduct: ProductModel = this.productForm.value;
      this.productService.createProduct(newProduct).subscribe(() => {
        this.loadProducts();
        this.closeModal();
      });
    }
  }

  closeModal(): void {
    const modal = document.getElementById('addProductModal');
    if (modal) {
      (window as any).$(modal).modal('hide');
    }
  }
}
