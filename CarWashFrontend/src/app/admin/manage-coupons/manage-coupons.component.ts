import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PromoCode } from './promo-code.model';

@Component({
  selector: 'app-manage-coupons',
  templateUrl: './manage-coupons.component.html',
  imports: [FormsModule, CommonModule],
  standalone: true
})
export class ManageCouponsComponent implements OnInit {
  promoCodes: PromoCode[] = [];
  newPromoCode: PromoCode = { code: '', discountPercent: 0, validTill: '' };
  editingPromo: PromoCode | null = null;

  private apiUrl = 'https://localhost:7266/api/PromoCode';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getAllPromoCodes();
  }

  getAllPromoCodes(): void {
  this.http.get<any>(this.apiUrl).subscribe(response => {
    this.promoCodes = response.$values; 
  });
}

  addPromoCode(): void {
    this.http.post<PromoCode>(this.apiUrl, this.newPromoCode).subscribe(() => {
      this.newPromoCode = { code: '', discountPercent: 0, validTill: '' };
      this.getAllPromoCodes();
    });
  }

  editPromoCode(promo: PromoCode): void {
    this.editingPromo = { ...promo };
  }

  cancelEdit(): void {
    this.editingPromo = null;
  }

  updatePromoCode(): void {
    if (this.editingPromo?.id) {
      this.http.put<PromoCode>(`${this.apiUrl}/${this.editingPromo.id}`, this.editingPromo)
        .subscribe(() => {
          this.editingPromo = null;
          this.getAllPromoCodes();
        });
    }
  }

  deletePromoCode(id: number | undefined): void {
    if (!id) return;
    this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => this.getAllPromoCodes());
  }
}
