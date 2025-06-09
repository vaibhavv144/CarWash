import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-redirect',
  standalone: true,
  imports: [CommonModule],
  template: `<p class="text-center mt-10 text-lg text-gray-600">Redirecting...</p>`
})
export class RedirectComponent {}
