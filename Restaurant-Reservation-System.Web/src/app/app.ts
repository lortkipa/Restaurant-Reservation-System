import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from "./components/header/header";
import { Footer } from "./components/footer/footer";
import { Home } from "./components/home/home";
import { Restaurants } from './components/restaurants/restaurants';
import { Restaurant } from './components/restaurant/restaurant';
import { Hero } from './components/hero/hero';

@Component({
  standalone: true,
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer, Home, Restaurants, Restaurant, Hero],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('Restaurant-Reservation-System.Web');
}
