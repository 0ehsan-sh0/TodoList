import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CategoriesService } from '../../services/categories/categories.service';

@Component({
  selector: 'app-create-category',
  imports: [FormsModule],
  templateUrl: './create-category.component.html',
  styleUrl: './create-category.component.css',
})
export class CreateCategoryComponent {
  @Output() created = new EventEmitter<void>();
  constructor(private categoryService: CategoriesService) {}
  onSubmit(form: NgForm) {
     this.categoryService.Create(form.value);
     this.created.emit();
     form.reset();
  }
}
