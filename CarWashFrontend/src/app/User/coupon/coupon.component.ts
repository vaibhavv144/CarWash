import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

interface Coupon {
  id: number;
  code: string;
  discountPercent: number;
  isActive: boolean;
  validTill: string;
}

@Component({
  selector: 'app-select-coupon',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule],
  templateUrl: './coupon.component.html',
})
export class SelectCouponComponent implements OnInit {
  coupons: Coupon[] = [];
  selectedCouponId: number | null = null;
  error = '';
  loading = false;

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.loading = true;
    this.http.get<any>('https://localhost:7266/api/PromoCode').subscribe({
      next: (res) => {
        this.coupons = res.$values.filter((c: Coupon) => c.isActive);
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load coupons.';
        this.loading = false;
      }
    });
  }

  selectCoupon(coupon: Coupon) {
    this.selectedCouponId = coupon.id;
    localStorage.setItem('selectedCoupon', JSON.stringify(coupon));
  }

  continue() {
    this.router.navigate(['/confirm-order']); // Use router instead of window.location.href
  }
}
