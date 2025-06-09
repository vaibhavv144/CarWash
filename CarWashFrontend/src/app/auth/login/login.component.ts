import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../core/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  handleLogin() {
    console.log('Login clicked');

    if (this.loginForm.invalid) return;

    this.authService.login(this.loginForm.value).subscribe({
      next: (res: any) => {
        console.log('Login response:', res);

        if (res.success && res.token) {
          localStorage.setItem('authToken', res.token);

          const decoded: any = jwtDecode(res.token);
          const role = decoded?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

          // const role = JSON.parse(atob(res.token));
          console.log('User Role:', role);

          if (role === 'Admin') {
            this.router.navigate(['/admin-dashboard']);
          } else if (role === 'User') {
            this.router.navigate(['/user-dashboard']);
          } else if (role === 'Washer') {
            this.router.navigate(['/washer-dashboard']);
          } else {
            this.router.navigate(['/login']);
          }
        } else {
          this.errorMessage = res.message || 'Login failed.';
        }
      },
      error: (err) => {
        console.error('Login error:', err);
        this.errorMessage = 'Login failed. Please check your credentials.';
      },
    });
  }
}
