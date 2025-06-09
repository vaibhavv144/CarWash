import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-manage-orders',
  imports: [CommonModule, HttpClientModule],
  templateUrl: './manage-orders.component.html'
  
})
export class ManageOrdersComponent implements OnInit {
  orders: any[] = [];
  apiUrl = 'https://localhost:7266/api/admin/orders'; // replace with actual URL

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getAllOrders();
  }

  getAllOrders(): void {
    this.http.get<any>(this.apiUrl).subscribe({
      next: (res) => {
        this.orders = res.$values || [];
      },
      error: (err) => {
        console.error('Failed to fetch orders', err);
      }
    });
  }
}
