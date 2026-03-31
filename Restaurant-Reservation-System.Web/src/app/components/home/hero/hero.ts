import { Component, EventEmitter, Output, output } from '@angular/core';
import { RouterLink } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-hero',
  imports: [RouterLink],
  templateUrl: './hero.html',
  styleUrl: './hero.scss',
})
export class Hero {
  description:string = "Exceptional restaurants, one seamless reservation. Create a free account and secure your perfect evening."

  
  scrollToRestaurants() {
    const el = document.getElementById('choose');
    el?.scrollIntoView({ behavior: 'smooth' });
  }
}
