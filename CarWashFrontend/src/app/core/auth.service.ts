// // auth.service.ts
// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   private baseUrl = 'https://localhost:7266/api/Account'; // ✅ CORRECT (from launchSettings.json)

//   constructor(private http: HttpClient) {}

//   login(data: any) {
//     return this.http.post(`${this.baseUrl}/Login`, data);
//   }

//   signup(data: any) {
//     return this.http.post(`${this.baseUrl}/Register`, data);
//   }
// }

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7266/api/Account';

  constructor(private http: HttpClient) {}

  login(data: any) {
    return this.http.post(`${this.baseUrl}/Login`, data);
  }

  signup(data: any) {
    return this.http.post(`${this.baseUrl}/Register`, data);
  }

  logout() {
    localStorage.removeItem('authToken');
  }

  // ✅ Returns the JWT token from local storage
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // ✅ Decodes and returns the user role from JWT
  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      return decoded?.role || null;
    } catch {
      return null;
    }
  }
}
