import { Component, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-washer-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './washer-dashboard.component.html',
  styleUrls: ['./washer-dashboard.component.css']
})
export class WasherDashboardComponent {
  isSidebarOpen = signal(false);
  isLargeScreen = computed(() => window.innerWidth >= 768);

  toggleSidebar() {
    this.isSidebarOpen.update(value => !value);
  }

  logout() {
    localStorage.removeItem('authToken');
    this.router.navigate(['/login']);
  }

  goToProfile() {
    this.router.navigate(['/washer/profile']);
  }

  constructor(private router: Router) {}
}
