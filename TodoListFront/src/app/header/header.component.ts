import { Component, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from '../models/jwtPayload.models';

@Component({
  selector: 'app-header',
  imports: [RegisterComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  isLoggedIn = false;
  username = "";
  onRegistered() {
    const dialog = document.getElementById('register') as HTMLDialogElement;
    dialog?.close();
    this.isLoggedIn = true;
    const token = localStorage.getItem('accessToken');
    if (token) {
      this.isLoggedIn = true;
      const decoded = jwtDecode<JwtPayload>(token);
      this.username = decoded.name;
    }
  }

  ngOnInit(): void {
    const token = localStorage.getItem('accessToken');
    if (token) {
      this.isLoggedIn = true;
      const decoded = jwtDecode<JwtPayload>(token);
      this.username = decoded.name;
    }
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.isLoggedIn = false;
  }
}
