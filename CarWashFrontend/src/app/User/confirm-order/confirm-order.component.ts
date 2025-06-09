import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { OrderService } from './confirm-order.service';

interface AddOn {
  id: number;
  name: string;
  price: number;
}

interface Coupon {
  id: number;
  code: string;
  discountPercent: number;
}

interface Car {
  id: number;
  make: string;
  model: string;
  year: number;
  userId: string;
  isActive: boolean;
}

interface ServicePackage {
  id: number;
  name: string;
  description: string;
  price: number;
  isActive: boolean;
}

@Component({
  selector: 'app-confirm-order',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './confirm-order.component.html',
  providers: [OrderService],
})
export class ConfirmOrderComponent implements OnInit {
  selectedPackage: ServicePackage | null = null;
  selectedAddOns: AddOn[] = [];
  selectedCoupon: Coupon | null = null;
  cars: Car[] = [];
  selectedCar: string = '';
  totalAmount: number = 0;
  error: string = '';
  successMessage: string = '';

  constructor(private orderService: OrderService) {}

  ngOnInit() {
    this.loadSelections();
    this.fetchCars();
  }

  loadSelections() {
    const pkg = localStorage.getItem('selectedPackage');
    const addons = localStorage.getItem('selectedAddOns');
    const coupon = localStorage.getItem('selectedCoupon');

    if (pkg) this.selectedPackage = JSON.parse(pkg);
    if (addons) this.selectedAddOns = JSON.parse(addons);
    if (coupon) this.selectedCoupon = JSON.parse(coupon);

    this.calculateTotal();
  }

  fetchCars() {
    this.orderService.getUserCars().subscribe({
      next: (cars) => (this.cars = cars),
      error: () => (this.error = 'Failed to load cars'),
    });
  }

  calculateTotal() {
    const addOnTotal = this.selectedAddOns.reduce((sum, addon) => sum + addon.price, 0);
    const packagePrice = this.selectedPackage?.price || 0;
    const discount = this.selectedCoupon ? this.selectedCoupon.discountPercent : 0;
    const totalBeforeDiscount = packagePrice + addOnTotal;
    this.totalAmount = totalBeforeDiscount - (totalBeforeDiscount * discount) / 100;
  }

  placeOrder() {
    const body = {
      packageName: this.selectedPackage?.name || '',
      addOnService: this.selectedAddOns.length > 0 ? this.selectedAddOns[0].name : '',
      promoCode: this.selectedCoupon?.code || '',
      car: this.selectedCar,
    };

    this.orderService.placeOrder(body).subscribe({
      next: (response) => {
        this.successMessage = 'Order placed successfully!';
        
        const orderId = response.orderId;
         const amount = response.totalAmount;
         
        localStorage.setItem('orderId', orderId.toString());
        localStorage.setItem('amount', amount.toString());
       // this.router.navigate(['/payment']);

        localStorage.removeItem('selectedPackage');
        localStorage.removeItem('selectedAddOns');
        localStorage.removeItem('selectedCoupon');
      },
      error: () => {
        this.error = 'Failed to place order.';
      },
    });
  }
}
