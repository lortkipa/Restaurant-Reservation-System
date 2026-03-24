import { Component, ElementRef, ViewChild } from '@angular/core';
import { Hero } from "../hero/hero";
import { Restaurants } from "../restaurants/restaurants";
import { ReservationForm } from "../reservation-form/reservation-form";

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [Hero, Restaurants, ReservationForm],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {

}
