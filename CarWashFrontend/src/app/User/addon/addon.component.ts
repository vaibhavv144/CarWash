import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule, Router } from '@angular/router';

interface AddOn {
  id: number;
  name: string;
  price: number;
  isActive: boolean;
}

@Component({
  selector: 'app-addon',
  standalone: true,
  imports: [CommonModule, HttpClientModule, RouterModule],
  templateUrl: './addon.component.html',
  styleUrls: ['./addon.component.css']
})
export class AddonComponent implements OnInit {
  addons: AddOn[] = [];
  selectedAddOnIds: number[] = [];
  loading = false;
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.loading = true;
    this.http.get<{ $id: string; $values: AddOn[] }>('https://localhost:7266/api/AddOn')
      .subscribe({
        next: (res) => {
          this.addons = res.$values.filter(addon => addon.isActive);
          this.loading = false;
        },
        error: () => {
          this.error = 'Failed to load add-ons.';
          this.loading = false;
        }
      });
  }

  toggleAddOn(id: number) {
    if (this.selectedAddOnIds.includes(id)) {
      this.selectedAddOnIds = this.selectedAddOnIds.filter(aid => aid !== id);
    } else {
      this.selectedAddOnIds.push(id);
    }
  }

  continue() {
    const selectedAddOns = this.addons.filter(addon => this.selectedAddOnIds.includes(addon.id));
    localStorage.setItem('selectedAddOns', JSON.stringify(selectedAddOns));
    this.router.navigate(['/select-coupon']); // Next route after add-ons
  }

  isSelected(id: number): boolean {
    return this.selectedAddOnIds.includes(id);
  }
}
