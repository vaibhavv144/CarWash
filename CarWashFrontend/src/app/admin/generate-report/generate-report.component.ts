import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-generate-report',
  imports: [CommonModule, FormsModule],
  templateUrl: './generate-report.component.html',
})
export class GenerateReportComponent {
  userId: string = '';
  loading = false;
  error: string | null = null;

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    if (!token) throw new Error('Missing authentication token');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }

  downloadReport(): void {
    this.error = null;
    if (!this.userId.trim()) {
      this.error = 'Please enter a valid user ID.';
      return;
    }

    this.loading = true;

    const url = `https://localhost:7266/api/admin/reportgenerate?userId=${this.userId}`;
    this.http.get(url, {
      headers: this.getAuthHeaders(),
      responseType: 'blob',
    }).subscribe({
      next: (blob) => {
        const link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = 'invoice.pdf';
        link.click();
        URL.revokeObjectURL(link.href);
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to generate report. Please check the user ID or try again later.';
        this.loading = false;
      }
    });
  }
}
