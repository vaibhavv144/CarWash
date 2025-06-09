import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';

export interface ServicePackage {
  id: number;
  name: string;
  description: string;
  price: number;
  isActive: boolean;
}

@Component({
  selector: 'app-manage-packages',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './manage-packages.component.html',
  styleUrls: ['./manage-packages.component.css']
})
export class ManagePackagesComponent implements OnInit {
  packages: ServicePackage[] = [];
  newPackage: ServicePackage = { id: 0, name: '', description: '', price: 0, isActive: true };
  editingPackage: ServicePackage | null = null;
  apiUrl = 'https://localhost:7266/api/ServicePackage';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getAllPackages();
  }

getAllPackages(): void {
  this.http.get<any>(this.apiUrl).subscribe((res) => {
    this.packages = res.$values; // Correctly access the array
  });
}

  addPackage(): void {
    this.http.post<ServicePackage>(this.apiUrl, this.newPackage).subscribe(() => {
      this.newPackage = { id: 0, name: '', description: '', price: 0, isActive: true };
      this.getAllPackages();
    });
  }

  editPackage(pkg: ServicePackage): void {
    this.editingPackage = { ...pkg }; // Make a copy
  }

  updatePackage(): void {
    if (!this.editingPackage) return;
    this.http.put(`${this.apiUrl}/${this.editingPackage.id}`, this.editingPackage).subscribe(() => {
      this.editingPackage = null;
      this.getAllPackages();
    });
  }

  deletePackage(id: number): void {
  if (confirm('Are you sure you want to delete this package?')) {
    this.http.delete(`${this.apiUrl}/${id}`, { responseType: 'json' }).subscribe({
      next: (res) => {
        console.log('Delete response:', res);
        this.getAllPackages();
      },
      error: (err) => {
        console.error('Delete failed:', err.error); // Log backend error
        alert('Delete failed: ' + (err.error?.message || 'Unknown error'));
      }
    });
  }
}


  cancelEdit(): void {
    this.editingPackage = null;
  }
}
