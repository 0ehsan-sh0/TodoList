import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from '../../models/register.model';
import { AuthResponse } from '../../models/auth-response.model';
import { BehaviorSubject } from 'rxjs';
import { LoginModel } from '../../models/login.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = 'https://localhost:7157/api/account';
  constructor(private http: HttpClient) {}

  register(data: RegisterModel) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, data);
  }

  login(data: LoginModel) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, data);
  }
}
