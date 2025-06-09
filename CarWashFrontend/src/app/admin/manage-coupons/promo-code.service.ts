import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PromoCode } from './promo-code.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PromoCodeService {
  private apiUrl = 'https://localhost:7266/api/PromoCode';

  constructor(private http: HttpClient) {}

  getAllPromoCodes(): Observable<PromoCode[]> {
    return this.http.get<PromoCode[]>(this.apiUrl);
  }

  addPromoCode(promoCode: PromoCode): Observable<PromoCode> {
    return this.http.post<PromoCode>(this.apiUrl, promoCode);
  }

  updatePromoCode(id: number, promoCode: PromoCode): Observable<PromoCode> {
    return this.http.put<PromoCode>(`${this.apiUrl}/${id}`, promoCode);
  }

  deletePromoCode(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
