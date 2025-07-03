import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoriesService } from '../services/categories/categories.service';
import { Category } from '../models/category.model';
import { AuthStateService } from '../services/auth/auth-state.service';
import { LucideAngularModule, Plus, Pencil, Trash } from 'lucide-angular';
import { ModalComponent } from '../modal/modal.component';
import { CreateCategoryComponent } from './create-category/create-category.component';
import { CommonModule } from '@angular/common';
import { UpdateCategoryComponent } from './update-category/update-category.component';

@Component({
  selector: 'app-categories',
  imports: [
    LucideAngularModule,
    ModalComponent,
    CreateCategoryComponent,
    CommonModule,
    UpdateCategoryComponent,
  ],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  readonly Plus = Plus;
  readonly Pencil = Pencil;
  readonly Trash = Trash;
  deleteId: number = 0;
  isLoggedIn = false;
  activeTab = 'createCategory';
  @ViewChild('createCategory') createCategoryModal!: ModalComponent;
  @ViewChild('editCategory') editCategoryModal!: ModalComponent;
  @ViewChild('deleteCategory') deleteCategoryModal!: ModalComponent;

  constructor(
    private categoryService: CategoriesService,
    private authState: AuthStateService
  ) {}

  ngOnInit(): void {
    this.authState.loggedIn$.subscribe((isLoggedIn) => {
      this.isLoggedIn = isLoggedIn;
      if (this.isLoggedIn) {
        this.categoryService.getCategories();
      } else {
        this.categories = [];
      }
    });
    this.categoryService.categories.subscribe((categories) => {
      this.categories = categories;
    });
  }

  edit(id: number) {
    this.categoryService.getCategory(id);
    this.editCategoryModal.open();
  }

  delete(id: number) {
    this.deleteCategoryModal.open();
    this.deleteId = id;
  }

  deleteConfirmed() {
    this.categoryService.delete(this.deleteId);
    this.closeDialog('deleteCategoryModal');
  }

  create() {
    this.createCategoryModal.open();
  }

  closeDialog(tab: string) {
    switch (tab) {
      case 'createCategoryModal':
        this.createCategoryModal.close();
        break;
      case 'editCategoryModal':
        this.editCategoryModal.close();
        break;
      case 'deleteCategoryModal':
        this.deleteCategoryModal.close();
        break;
    }
  }
}
