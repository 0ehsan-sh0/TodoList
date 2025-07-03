import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Category, CategoryCreateRequest } from '../../models/category.model';
import { AlertService } from '../../uiService/alert.service';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  private readonly apiUrl = 'https://localhost:7157/api/category';

  constructor(private http: HttpClient, private alertService: AlertService) {}

  categories = new BehaviorSubject<Category[]>([]);
  category = new BehaviorSubject<Category>({} as Category);

  getCategories() {
    this.http.get<Category[]>(this.apiUrl).subscribe({
      next: (categories) => {
        this.categories.next([...categories]);
      },
      error: (err) => {
        this.handleError(err);
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
        this.handleError(err);
      },
    });
  }

  Create(data: CategoryCreateRequest) {
    return this.http.post<Category>(`${this.apiUrl}`, data).subscribe({
      next: (category) => {
        this.categories.next([category, ...this.categories.value]);
        this.alertService.show('دسته بندی اضافه شد', 'success');
      },
      error: (err) => {
        this.handleError(err);
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
        this.alertService.show('دسته بندی به روز شد', 'success');
      },
      error: (err) => {
        this.handleError(err);
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
        this.alertService.show('دسته بندی حذف شد', 'success');
      },
      error: (err) => {
        this.handleError(err);
      },
    });
  }

  private handleError(error: HttpErrorResponse) {
    switch (error.status) {
      case 401:
        this.alertService.show('لطفا وارد حساب خود شوید', 'error');
        break;
      case 404:
        this.alertService.show('دسته بندی یافت نشد', 'error');
        break;
      case 400:
        const errors = error?.error?.errors;
        if (errors && typeof errors === 'object') {
          const messages = Object.values(errors).flat(); // flattens all arrays into one
          if (messages.length) {
            this.alertService.show(messages[0] as string, 'error'); // show first error
          } else {
            this.alertService.show('لطفا اطلاعات صحیح وارد کنید', 'error');
          }
        } else {
          this.alertService.show('لطفا اطلاعات صحیح وارد کنید', 'error');
        }
        break;
      default:
        this.alertService.show(
          'مشکلی پیش آمد. لطفاً دوباره تلاش کنید.',
          'error'
        );
        break;
    }
  }
}
