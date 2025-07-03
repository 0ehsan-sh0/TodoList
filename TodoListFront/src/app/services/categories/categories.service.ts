import { HttpClient, HttpParams } from '@angular/common/http';
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
  category = new BehaviorSubject<Category>({} as Category);

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

  getCategory(id: number) {
    const params = new HttpParams().set('todos', false);
    this.http.get<Category>(`${this.apiUrl}/${id}`, { params }).subscribe({
      next: (category) => {
        this.category.next(category);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  Create(data: CategoryCreateRequest) {
    return this.http.post<Category>(`${this.apiUrl}`, data).subscribe({
      next: (category) => {
        this.categories.next([category, ...this.categories.value]);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  update(id: number, data: CategoryCreateRequest) {
    return this.http.put<Category>(`${this.apiUrl}/${id}`, data).subscribe({
      next: (category) => {
        this.categories.next(
          this.categories.value.map((c) => {
            if (c.id === id) {
              return category;
            }
            return c;
          })
        );
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => {
        this.categories.next(
          this.categories.value.filter((c) => {
            return c.id !== id;
          })
        );
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
