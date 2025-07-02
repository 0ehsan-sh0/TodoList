import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject } from 'rxjs';
import { JwtPayload } from '../../models/jwtPayload.models';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {

 private loggedInSubject = new BehaviorSubject<boolean>(this.hasToken());
  public loggedIn$ = this.loggedInSubject.asObservable();

  constructor() {}

  private hasToken(): boolean {
    const token = localStorage.getItem('accessToken');
    return !!token;
  }

  getUsername(): string | null {
    const token = localStorage.getItem('accessToken');
    if (token) {
      const decoded = jwtDecode<JwtPayload>(token);
      return decoded.name;
    }
    return null;
  }

  login(token: string) {
    localStorage.setItem('accessToken', token);
    this.loggedInSubject.next(true);
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.loggedInSubject.next(false);
  }
}
