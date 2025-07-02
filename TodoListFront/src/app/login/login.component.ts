import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth/auth.service';
import { AuthStateService } from '../services/auth/auth-state.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  @Output() loggedIn = new EventEmitter<void>();
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private authState: AuthStateService
  ) {}

  onSubmit(formData: NgForm) {
    this.errorMessage = '';

    this.authService.login(formData.value).subscribe({
      next: (res) => {
        if (res.accessToken) {
          this.authState.login(res.accessToken);
          formData.reset();
          this.loggedIn.emit();
        }
      },
      error: (err) => {
        if (err.status === 401) {
          this.errorMessage = 'نام کاربری یا رمز عبور نادرست است.';
        } else {
          this.errorMessage = 'مشکلی پیش آمد. لطفاً دوباره تلاش کنید.';
        }
      },
    });
  }
}
