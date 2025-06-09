import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor, NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Washer, WasherInputDto, WasherService } from './washer.service';

@Component({
  selector: 'app-manage-washers',
  templateUrl: './manage-washers.component.html',
  standalone: true,
  imports: [
    NgIf,
    NgFor,
    NgClass,
    FormsModule
  ]
})
export class ManageWashersComponent implements OnInit {
  washers: Washer[] = [];
  loading = false;
  error: string | null = null;

  newWasher: WasherInputDto = {
    name: '',
    email: '',
    isActive: true,
    isAvailable: true
  };

  constructor(private washerService: WasherService) {}

  ngOnInit(): void {
    this.fetchWashers();
  }

  fetchWashers(): void {
    this.loading = true;
    this.washerService.getAllWashers().subscribe({
      next: (res) => {
        this.washers = res.$values;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load washers';
        this.loading = false;
      }
    });
  }

  addWasher(): void {
    this.washerService.addOrEditWasher(this.newWasher).subscribe({
      next: () => {
        this.fetchWashers();
        this.newWasher = { name: '', email: '', isActive: true, isAvailable: true };
      },
      error: () => this.error = 'Failed to add washer'
    });
  }

  deleteWasher(id: string): void {
    if (!confirm('Are you sure you want to delete this washer?')) return;

    this.washerService.deleteWasher(id).subscribe({
      next: () => this.fetchWashers(),
      error: () => this.error = 'Failed to delete washer'
    });
  }
}
