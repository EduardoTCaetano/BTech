import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductService } from '../../../services/product/product.service';
import { CategoryService } from '../../../services/category/category.service';
import { ProductModel } from '../../../model/product';
import { Category } from '../../../model/category';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  products: ProductModel[] = [];
  categories: Category[] = [];
  productForm: FormGroup;
  editProductForm: FormGroup;
  selectedProduct: ProductModel | null = null;

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private fb: FormBuilder
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      image: ['', Validators.required],
      categoryId: ['', Validators.required]
    });

    this.editProductForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      image: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe((data: ProductModel[]) => {
      this.products = data;
    });
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe((data: Category[]) => {
      this.categories = data;
    });
  }

  openModal(): void {
    const modal = document.getElementById('addProductModal');
    if (modal) {
      (window as any).$(modal).modal('show');
    }
  }

  openEditModal(product: ProductModel): void {
    this.selectedProduct = product;
    this.editProductForm.patchValue(product);
    const modal = document.getElementById('editProductModal');
    if (modal) {
      (window as any).$(modal).modal('show');
    }
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const newProduct: ProductModel = this.productForm.value;
      this.productService.createProduct(newProduct).subscribe(() => {
        this.loadProducts();
        this.productForm.reset();
        this.closeModal('addProductModal');
      });
      Swal.fire({
        title: 'Produto criado!',
        text: 'O produto foi criada com sucesso.',
        icon: 'success',
        confirmButtonText: 'OK'
      });
    }
  }

  onEditSubmit(): void {
    if (this.editProductForm.valid && this.selectedProduct && this.selectedProduct.id) {
      const updatedProduct = {
        ...this.selectedProduct,
        ...this.editProductForm.value
      };

      this.productService.updateProduct(this.selectedProduct.id, updatedProduct).subscribe(() => {
        this.loadProducts();
        this.closeModal('editProductModal');
      });
    }
  }

  closeModal(modalId: string): void {
    const modal = document.getElementById(modalId);
    if (modal) {
      (window as any).$(modal).modal('hide');
    }
  }

  confirmDelete(product: ProductModel): void {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
      title: "Tem certeza?",
      text: "Você não poderá reverter essa ação!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Sim, exclua!",
      cancelButtonText: "Não, cancelar!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.deleteProduct(product);
        swalWithBootstrapButtons.fire({
          title: "Excluído!",
          text: "O produto foi excluído com sucesso.",
          icon: "success"
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        swalWithBootstrapButtons.fire({
          title: "Cancelado",
          text: "O produto está seguro :)",
          icon: "error"
        });
      }
    });
  }

  deleteProduct(product: ProductModel): void {
    this.productService.deleteProduct(product.id!).subscribe(() => {
      this.loadProducts();
    });
  }

}
