import { Component, EventEmitter, OnInit, output, Output, ViewChild } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from '../models/jwtPayload.models';
import { RegisterComponent } from '../register/register.component';
import { LoginComponent } from '../login/login.component';
import { AuthStateService } from '../services/auth/auth-state.service';
import { LucideAngularModule, LogOut } from 'lucide-angular';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RegisterComponent, LoginComponent,LucideAngularModule,ModalComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  public isLoggedIn = false;
  readonly logOut = LogOut;
  username = '';
  @ViewChild('registerModal') registerModal!: ModalComponent;
  @ViewChild('loginModal') loginModal!: ModalComponent;
  @Output() loggedOut = new EventEmitter<void>();
 
  constructor(private authState: AuthStateService) {
    
  }
  
  ngOnInit(): void {
    this.authState.loggedIn$.subscribe((isLoggedIn) => {
      this.isLoggedIn = isLoggedIn;
    });
    this.checkAuth();
  }

  checkAuth() {
    this.username = this.authState.getUsername() ?? '';
  }

  openDialog(tab:string) {
    if(tab === 'register') {
    this.registerModal.open();
    } else {
      this.loginModal.open();
    }
  }

  onRegistered() {
    this.registerModal.close();
    this.checkAuth();
  }

  onLoggedIn() {
    this.loginModal.close();
    this.checkAuth();
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.authState.logout();
    this.username = '';
    this.loggedOut.emit();
  }
}
