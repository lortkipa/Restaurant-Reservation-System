import { Component, Input } from '@angular/core';
import { DishModel } from '../../../models/menu-model';

@Component({
  standalone: true,
  selector: 'app-dish',
  imports: [],
  templateUrl: './dish.html',
  styleUrl: './dish.scss',
})
export class Dish {
  @Input() dish !: DishModel
}
