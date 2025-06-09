import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  orderId: string | null = '';
  amount: string | null = '';
  successMessage = '';
  errorMessage = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.orderId = localStorage.getItem('orderId');
    this.amount = localStorage.getItem('amount');
  }

  makePayment(): void {
    const token = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    const paymentData = {
      orderId: Number(this.orderId),
      amountPaid: Number(this.amount)
    };

    this.http.post('https://localhost:7266/api/Payment', paymentData, { headers })
      .subscribe({
        next: (res) => {
          this.successMessage = 'Payment successful!';
          localStorage.removeItem('orderId');
          localStorage.removeItem('amount');
        },
        error: (err) => {
          this.errorMessage = 'Payment failed. Please try again.';
        }
      });
  }
}
