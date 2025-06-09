import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddonService } from './addon.service';
import { AddOn, AddOnCreateDto } from './addon.model';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-manage-addons',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './manage-addons.component.html',
})
export class ManageAddonsComponent implements OnInit {
  addons: AddOn[] = [];
  newAddon: AddOnCreateDto = { name: '', price: 0, isActive: true };
  editingAddon: AddOn | null = null;

  constructor(private addonService: AddonService) {}

  ngOnInit(): void {
    this.loadAddons();
  }

  // private getAuthHeaders(): HttpHeaders {
  //       const token = localStorage.getItem('authToken');
  //       if (!token) {
  //         throw new Error('Missing authentication token');
  //       }
  //       return new HttpHeaders({
  //         Authorization: `Bearer ${token}`,
  //         'Content-Type': 'application/json',
  //       });
  //     }

  loadAddons(): void {
  this.addonService.getAllAddons().subscribe((addons) => {
    this.addons = addons;
  });
}

  startEdit(addon: AddOn): void {
    this.editingAddon = { ...addon };
  }

  cancelEdit(): void {
    this.editingAddon = null;
  }

  saveEdit(): void {
    if (!this.editingAddon) return;

    const { id, name, price, isActive } = this.editingAddon;
    this.addonService.updateAddon(id, { name, price, isActive }).subscribe(() => {
      this.loadAddons();
      this.editingAddon = null;
    });
  }

  addAddon(): void {
    this.addonService.addAddon(this.newAddon).subscribe(() => {
      this.newAddon = { name: '', price: 0, isActive: true };
      this.loadAddons();
    });
  }

  deleteAddon(id: number): void {
    this.addonService.deleteAddon(id).subscribe(() => {
      this.loadAddons();
    });
  }
}
