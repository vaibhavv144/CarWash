import { Component, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {
  isSidebarOpen = true;
  isLargeScreen = window.innerWidth >= 768;

  constructor(private authService: AuthService, private router: Router) {}

  @HostListener('window:resize', [])
  onResize() {
    this.isLargeScreen = window.innerWidth >= 768;
    if (this.isLargeScreen) {
      this.isSidebarOpen = true;
    }
  }

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

    goToProfile() {
    this.router.navigate(['/admin/profile']);
  }
}
