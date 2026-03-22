import { Component } from '@angular/core';
import { Hero } from "../hero/hero";
import { Restaurants } from "../restaurants/restaurants";

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [Hero, Restaurants],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {}
