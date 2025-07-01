import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth/auth.service';
import { AuthResponse } from '../models/auth-response.model';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  isPasswordMatch: boolean = false;
  @Output() registered = new EventEmitter<void>();
  constructor(private authService: AuthService) {}

  onSubmit(formData: NgForm) {
    if (formData.value.password !== formData.value.passwordConfirmation) return;
    else {
      this.isPasswordMatch = true;
      this.authService.register(formData.value).subscribe((res) => {
        if (res.accessToken) {
          localStorage.setItem('accessToken', res.accessToken);
          this.registered.emit();
        }
      });
    }
  }
}
