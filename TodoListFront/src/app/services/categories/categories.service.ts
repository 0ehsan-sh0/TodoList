import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Category } from '../../models/category.model';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private readonly apiUrl = 'https://localhost:7157/api/category';

  constructor(private http: HttpClient) {}

  categories = new BehaviorSubject<Category[]>([]);

  getCategories() {
    this.http.get<Category[]>(this.apiUrl).subscribe((categories) => {
      this.categories.next([...categories]);
    });
  }
}
