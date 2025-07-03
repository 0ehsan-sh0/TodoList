import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth/auth.service';
import { AuthResponse } from '../models/auth-response.model';
import { AuthStateService } from '../services/auth/auth-state.service';
import { AlertService } from '../uiService/alert.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  errorMessage = '';
  isPasswordMatch: boolean = false;
  @Output() registered = new EventEmitter<void>();
  constructor(
    private authService: AuthService,
    private authState: AuthStateService,
    private alertService: AlertService
  ) {}

  onSubmit(formData: NgForm) {
    if (formData.value.password !== formData.value.passwordConfirmation) return;
    else {
      this.isPasswordMatch = true;
      this.authService.register(formData.value).subscribe({
        next: (res) => {
          if (res.accessToken) {
            this.authState.login(res.accessToken);
            this.registered.emit();
            this.alertService.show('ثبت نام موفقیت آمیز بود.');
          }
        },
        error: (err) => {
          if (err.status === 409) {
            this.errorMessage = 'نام کاربری قبلاً ثبت شده است.';
          } else {
            this.errorMessage = 'مشکلی پیش آمد. لطفاً دوباره تلاش کنید.';
          }
        },
      });
      formData.reset();
    }
  }
}
