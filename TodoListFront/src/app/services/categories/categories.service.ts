import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Category, CategoryCreateRequest } from '../../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private readonly apiUrl = 'https://localhost:7157/api/category';

  constructor(private http: HttpClient) {}

  categories = new BehaviorSubject<Category[]>([]);

  getCategories() {
    this.http.get<Category[]>(this.apiUrl).subscribe({
      next: (categories) => {
        this.categories.next([...categories]);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  Create(data: CategoryCreateRequest) {
    return this.http.post<Category>(`${this.apiUrl}`, data).subscribe({
      next: (category) => {
        this.categories.next([category,...this.categories.value]);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
