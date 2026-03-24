import { Component, EventEmitter, Output } from '@angular/core';
import { Restaurant } from "../restaurant/restaurant";
import { RestaurantService } from '../../services/restaurant-service';
import { RestaurantModel } from '../../models/restaurant-model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-restaurants',
  imports: [Restaurant, CommonModule],
  templateUrl: './restaurants.html',
  styleUrl: './restaurants.scss',
})
export class Restaurants {
  constructor(private restaurantService : RestaurantService) {}

  restaurants:RestaurantModel[] = [];

  ngOnInit() {
    this.restaurantService.getAll().subscribe(data => {
      this.restaurants = data;
    });
  }
}
