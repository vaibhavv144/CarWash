import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-customers',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-customers.component.html',
  styleUrls: ['./manage-customers.component.scss']
})
export class ManageCustomersComponent implements OnInit {
  customers: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchCustomers();
  }
private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    if (!token) {
      throw new Error('Missing authentication token');
    }
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }
  fetchCustomers() {
    const headers = this.getAuthHeaders();
    this.http.get<any>('https://localhost:7266/api/admin/customers',{headers}).subscribe({
      next: (res) => {
        if (res && res.$values) {
          this.customers = res.$values.map((c: any) => ({
            userName: c.userName,
            email: c.email,
            isActive: c.isActive,
            id: c.id
          }));
        }
      },
      error: (err) => {
        console.error('Failed to load customers:', err);
      }
    });
  }
}
