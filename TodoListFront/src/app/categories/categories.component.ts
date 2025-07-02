import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoriesService } from '../services/categories/categories.service';
import { Category } from '../models/category.model';
import { AuthStateService } from '../services/auth/auth-state.service';
import { LucideAngularModule, Plus } from 'lucide-angular';
import { ModalComponent } from '../modal/modal.component';
import { CreateCategoryComponent } from './create-category/create-category.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-categories',
  imports: [LucideAngularModule, ModalComponent, CreateCategoryComponent,CommonModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: Category[] = [];
  readonly Plus = Plus;
  isLoggedIn = false;
  activeTab = 'createCategory';
  @ViewChild('createCategory') createCategoryModal!: ModalComponent;

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

  openDialog(tab: string) {
    this.createCategoryModal.open();
  }

  closeDialog() {
    this.createCategoryModal.close();
  }
}
