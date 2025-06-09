import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WasherService } from '../washer.service';

@Component({
  selector: 'app-available-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './available-orders.component.html',
  styleUrls: ['./available-orders.component.css']
})
export class AvailableOrdersComponent implements OnInit {
  orders: any[] = [];

  constructor(private washerService: WasherService) {}

  ngOnInit() {
    this.washerService.getAvailableOrders().subscribe(data => this.orders = data as any[]);
  }

  accept(orderId: number) {
    this.washerService.acceptOrder(orderId).subscribe(() => this.ngOnInit());
  }

  reject(orderId: number) {
    this.washerService.rejectOrder(orderId).subscribe(() => this.ngOnInit());
  }
}
