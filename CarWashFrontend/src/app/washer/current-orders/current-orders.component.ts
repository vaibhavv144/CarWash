import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WasherService } from '../washer.service';

@Component({
  selector: 'app-current-orders',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './current-orders.component.html',
  styleUrls: ['./current-orders.component.css']
})
export class CurrentOrdersComponent implements OnInit {
  orders: any[] = [];
  statusUpdate: { [key: number]: { status: string; imageUrl: string } } = {};

  constructor(private washerService: WasherService) {}

  ngOnInit() {
    this.washerService.getCurrentOrders().subscribe(data => this.orders = data as any[]);
  }

  updateStatus(orderId: number) {
    const { status, imageUrl } = this.statusUpdate[orderId] || {};
    if (!status) return alert('Please enter status');
    this.washerService.updateOrderStatus(orderId, status, imageUrl).subscribe(() => {
      alert('Status updated');
      this.ngOnInit();
    });
  }
}
