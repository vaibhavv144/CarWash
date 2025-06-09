import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = 'https://localhost:7266/api';//https://localhost:7266/api/User/GetAllCar
  private get authToken(): string {
    return localStorage.getItem('authToken') || '';
  }

  constructor(private http: HttpClient) {}

  getUserCars(): Observable<any[]> {
    return this.http
      .get<any>(`${this.apiUrl}/User/GetAllCar`, {
        headers: new HttpHeaders({ Authorization: `Bearer ${this.authToken}` }),
      })
      .pipe(map((res) => res.message.$values));
  }

  placeOrder(orderData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Order/book-now`, orderData, {
      headers: new HttpHeaders({ Authorization: `Bearer ${this.authToken}` }),
    });
  }
}
