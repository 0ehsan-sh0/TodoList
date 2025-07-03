import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CategoriesService } from '../../services/categories/categories.service';
import { Category } from '../../models/category.model';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-update-category',
  imports: [FormsModule],
  templateUrl: './update-category.component.html',
  styleUrl: './update-category.component.css',
})
export class UpdateCategoryComponent implements OnInit {
  @Output() updated = new EventEmitter<void>();
  category: Category = {} as Category;

  constructor(private categoryService: CategoriesService) {}

  ngOnInit() {
    this.categoryService.category.subscribe((category) => {
      this.category = category;
    });
  }

  onSubmit(form: NgForm) {
    this.categoryService.update(this.category.id, form.value);
    form.reset();
    this.updated.emit();
  }
}
