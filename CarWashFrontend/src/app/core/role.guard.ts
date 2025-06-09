// role.guard.ts
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const token = this.authService.getToken();
    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    try {
      const decoded: any = jwtDecode(token);

    
      const role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

      console.log('Decoded role:', role); 
      if (role === 'Admin') return true;
      if (role === 'User') return true;
      if (role === 'Washer') return true;

      this.router.navigate(['/login']);
      return false;
    } catch {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
