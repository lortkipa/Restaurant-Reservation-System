import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-hero',
  imports: [],
  templateUrl: './hero.html',
  styleUrl: './hero.scss',
})
export class Hero {
  description:string = "Exceptional restaurants, one seamless reservation. Create a free account and secure your perfect evening."
}
