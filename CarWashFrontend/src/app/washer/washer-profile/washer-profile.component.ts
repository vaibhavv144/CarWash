import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WasherService } from '../washer.service';

@Component({
  selector: 'app-washer-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './washer-profile.component.html',
  styleUrls: ['./washer-profile.component.css']
})
export class WasherProfileComponent implements OnInit {
  washer: any = {};
  washerId = localStorage.getItem('washerId') ?? '';

  constructor(private washerService: WasherService) {}

  ngOnInit() {
    this.washerService.getProfile(this.washerId).subscribe(data => this.washer = data);
  }

  updateProfile() {
    this.washerService.updateProfile(this.washerId, this.washer).subscribe(() => {
      alert('Profile updated!');
    });
  }
}
