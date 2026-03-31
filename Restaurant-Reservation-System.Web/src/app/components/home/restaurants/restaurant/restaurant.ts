import { Component, Input, input } from '@angular/core';
import { RouterLink, RouterModule } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-restaurant',
  imports: [RouterLink, RouterModule],
  templateUrl: './restaurant.html',
  styleUrl: './restaurant.scss',
})
export class Restaurant {
  @Input() id !: number
  @Input() name !: string
  @Input() location !: string
  @Input() description !: string
}
