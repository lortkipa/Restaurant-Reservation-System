import { Component } from '@angular/core';
import { Restaurant } from "../restaurant/restaurant";

@Component({
  selector: 'app-restaurants',
  imports: [Restaurant],
  templateUrl: './restaurants.html',
  styleUrl: './restaurants.scss',
})
export class Restaurants {}
