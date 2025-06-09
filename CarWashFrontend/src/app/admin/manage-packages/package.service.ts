// src/app/admin/manage-packages/package.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Package } from './package.model';

@Injectable({
  providedIn: 'root'
})
export class PackageService {
  private apiUrl = 'https://localhost:7266/api/ServicePackage'; // your backend URL

  constructor(private http: HttpClient) {}

  getPackages(): Observable<Package[]> {
    return this.http.get<Package[]>(this.apiUrl);
  }

  addPackage(pkg: Package): Observable<Package> {
    return this.http.post<Package>(this.apiUrl, pkg);
  }

  updatePackage(id: number, pkg: Package): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, pkg);
  }

  deletePackage(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
