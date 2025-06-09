import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CarService } from '../../core/car.service';

@Component({
  selector: 'app-add-car',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css'],
})
export class AddCarComponent {
  carForm: FormGroup;
  message = '';
  error = '';

  constructor(private fb: FormBuilder, private carService: CarService) {
    this.carForm = this.fb.group({
      make: ['', Validators.required],
      model: ['', Validators.required],
      year: ['', [Validators.required, Validators.pattern('^[0-9]{4}$')]],
    });
  }

  submit() {
    if (this.carForm.invalid) {
      return;
    }

    this.carService.addCar(this.carForm.value).subscribe({
      next: () => {
        this.message = 'Car added successfully!';
        this.carForm.reset();
      },
      error: (err) => {
        console.error(err);
        this.error = 'Failed to add car. Make sure you are logged in.';
      },
    });
  }
}
