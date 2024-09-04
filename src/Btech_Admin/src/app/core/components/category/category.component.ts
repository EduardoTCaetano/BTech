import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';
import { CategoryService } from '../../../services/category/category.service';
import { Category } from '../../../model/category';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = [];
  categoryForm: FormGroup;
  editCategoryForm: FormGroup;
  selectedCategory: Category | null = null;

  constructor(
    private categoryService: CategoryService,
    private fb: FormBuilder
  ) {
    this.categoryForm = this.fb.group({
      description: ['', Validators.required],
      isActive: [true]
    });

    this.editCategoryForm = this.fb.group({
      description: ['', Validators.required],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe((data: Category[]) => {
      this.categories = data;
    });
  }

  openCreateModal(): void {
    this.categoryForm.reset({ isActive: true });
    const modal = document.getElementById('addCategoryModal');
    if (modal) {
      (window as any).$(modal).modal('show');
    }
  }

  openEditModal(category: Category): void {
    this.selectedCategory = category;
    this.editCategoryForm.patchValue(category);
    const modal = document.getElementById('editCategoryModal');
    if (modal) {
      (window as any).$(modal).modal('show');
    }
  }

  onCreateCategory(): void {
    if (this.categoryForm.valid) {
      const newCategory: Category = this.categoryForm.value;
      this.categoryService.createCategory(newCategory).subscribe(() => {
        this.loadCategories();
        this.categoryForm.reset();
        this.closeModal('addCategoryModal');
        Swal.fire({
          title: 'Categoria criada!',
          text: 'A nova categoria foi criada com sucesso.',
          icon: 'success',
          confirmButtonText: 'OK'
        });
      });
    }
  }

  onEditCategory(): void {
    if (this.editCategoryForm.valid && this.selectedCategory && this.selectedCategory.id) {
      const updatedCategory = {
        ...this.selectedCategory,
        ...this.editCategoryForm.value
      };

      this.categoryService.updateCategory(this.selectedCategory.id, updatedCategory).subscribe(() => {
        this.loadCategories();
        this.closeModal('editCategoryModal');
        Swal.fire({
          title: 'Categoria atualizada!',
          text: 'A categoria foi atualizada com sucesso.',
          icon: 'success',
          confirmButtonText: 'OK'
        });
      });
    }
  }

  closeModal(modalId: string): void {
    const modal = document.getElementById(modalId);
    if (modal) {
      (window as any).$(modal).modal('hide');
    }
  }

  confirmDelete(category: Category): void {
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
      if (result.isConfirmed && category.id) {
        this.deleteCategory(category.id);
      }
      else if (result.dismiss === Swal.DismissReason.cancel) {
        swalWithBootstrapButtons.fire({
          title: "Cancelado",
          text: "A categoria está segura :)",
          icon: "error"
        });
      }
    });
  }

  deleteCategory(id: string): void {
    this.categoryService.deleteCategory(id).subscribe(() => {
      this.loadCategories();
      Swal.fire({
        title: 'Excluído!',
        text: 'A categoria foi excluída com sucesso.',
        icon: 'success'
      });
    });
  }
}
