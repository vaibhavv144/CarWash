
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Washer {
  id: string;
  userName: string;
  email: string;
  isActive: boolean;
  isAvailable: boolean;
}

export interface WasherInputDto {
  name: string;
  email: string;
  isAvailable: boolean;
  isActive: boolean;
}

export interface WasherResponse {
  $id: string;
  $values: Washer[];
}

@Injectable({
  providedIn: 'root'
})
export class WasherService {
   private washerUrl = 'https://localhost:7266/api/admin/washer';    // for POST (add/edit)
  private washersUrl = 'https://localhost:7266/api/admin/washers';

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('authToken');
    if (!token) throw new Error('Missing authentication token');
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }

  getAllWashers(): Observable<WasherResponse> {
    return this.http.get<WasherResponse>(this.washersUrl, { headers: this.getAuthHeaders() });
  }

  addOrEditWasher(dto: WasherInputDto): Observable<string> {
    return this.http.post(this.washerUrl, dto, {
      headers: this.getAuthHeaders(),
      responseType: 'text',
    });
  }

  deleteWasher(id: string): Observable<string> {
    return this.http.delete(`${this.washerUrl}/${id}`, {
      headers: this.getAuthHeaders(),
      responseType: 'text',
    });
  }
}
