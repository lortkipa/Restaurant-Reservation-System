import { Component, Input, input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-restaurant',
  imports: [],
  templateUrl: './restaurant.html',
  styleUrl: './restaurant.scss',
})
export class Restaurant {
  @Input() name !: string
  @Input() location !: string
  @Input() description !: string
}
