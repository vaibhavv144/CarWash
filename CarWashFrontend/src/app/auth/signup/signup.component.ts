import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';

import { Router, RouterLink } from '@angular/router';

import { AuthService } from '../../core/auth.service';
 
@Component({

  selector: 'app-signup',

  standalone: true,

  imports: [CommonModule, FormsModule, RouterLink],

  templateUrl: './signup.component.html',

  styleUrls: ['./signup.component.css']

})

export class SignupComponent {

  formData = {

    email: '',

    username: '',

    password: '',

    roles: [] as string[] // ✅ Matches backend: array of strings

  };
 
  constructor(private authService: AuthService, private router: Router) {}
 
  onSubmit(): void {

    // Ensure role is in array format

    const payload = {

      email: this.formData.email,

      username: this.formData.username,

      password: this.formData.password,

      roles: this.formData.roles

    };
 
    this.authService.signup(payload).subscribe({

      next: () => {

        alert('Signup successful');

        this.router.navigate(['/login']);

      },

      error: (err) => {

        alert('Signup failed: ' + (err.error?.message || err.message));

      }

    });

  }

}

 