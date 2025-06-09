import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class WasherService {
  private baseUrl = 'https://localhost:7266/api/Washer'; // update with your API
  private authHeaders = () => ({
    headers: new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('authToken')}`
    })
  });

  constructor(private http: HttpClient) {}

  getProfile(washerId: string) {
    return this.http.get(`${this.baseUrl}/profile/${washerId}`, this.authHeaders());
  }

  updateProfile(washerId: string, data: any) {
    return this.http.put(`${this.baseUrl}/update-profile/${washerId}`, data, this.authHeaders());
  }

  getAvailableOrders() {
    return this.http.get(`${this.baseUrl}/available-orders`, this.authHeaders());
  }

  acceptOrder(orderId: number) {
    return this.http.post(`${this.baseUrl}/accept/${orderId}`, {}, this.authHeaders());
  }

  rejectOrder(orderId: number) {
    return this.http.post(`${this.baseUrl}/reject/${orderId}`, {}, this.authHeaders());
  }

  getCurrentOrders() {
    return this.http.get(`${this.baseUrl}/current-orders`, this.authHeaders());
  }

  updateOrderStatus(orderId: number, status: string, imageUrl: string) {
    return this.http.put(`${this.baseUrl}/update-status/${orderId}?status=${status}&imageUrl=${imageUrl}`, {}, this.authHeaders());
  }
}
