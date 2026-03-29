import { Component } from '@angular/core';
import { RestaurantService } from '../../services/restaurant-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from '../../services/local-storage-service';

@Component({
  standalone: true,
  selector: 'app-reservation-form',
  imports: [FormsModule, CommonModule],
  templateUrl: './reservation-form.html',
  styleUrl: './reservation-form.scss',
})
export class ReservationForm {
Request : string = "has been requested. We'll send a confirmation to your email within 24 hours."

  IsLoggedIn : Boolean = false;

  restaurants: any[] = [];
  selectedRestaurantIndex: number | null = null;
  selectedRestaurant: any;

  constructor(private restaurantService: RestaurantService, private localStorage : LocalStorageService) {}

  ngOnInit(): void {
    this.IsLoggedIn = this.localStorage.getItem('token') != '';

    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurantService.getAll().subscribe({
      next: (data) => {
        this.restaurants = data;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  selectRestaurant(index: number) {
    this.selectedRestaurantIndex = index;
    this.selectedRestaurant = this.restaurants[index];
  }
}
