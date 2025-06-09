import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

interface ServicePackage {
  id: number;
  name: string;
  description: string;
  price: number;
  isActive: boolean;
}

@Component({
  selector: 'app-package-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule],
  templateUrl: './package-list.component.html',
  styleUrls: ['./package-list.component.css']
})
export class PackageListComponent implements OnInit {
  packages: ServicePackage[] = [];
  selectedPackageId: number | null = null;
  loading = false;
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.loading = true;
    this.http.get<{ $id: string; $values: ServicePackage[] }>('https://localhost:7266/api/ServicePackage')
      .subscribe({
        next: (res) => {
          this.packages = res.$values.filter(pkg => pkg.isActive);
          this.loading = false;
        },
        error: () => {
          this.error = 'Failed to load packages.';
          this.loading = false;
        }
      });
  }

  selectPackage(pkg: ServicePackage) {
    this.selectedPackageId = pkg.id;
    localStorage.setItem('selectedPackage', JSON.stringify(pkg));
  }

  continue() {
    if (this.selectedPackageId !== null) {
      this.router.navigate(['/addons']);
    }
  }

  isSelected(id: number): boolean {
    return this.selectedPackageId === id;
  }
}
