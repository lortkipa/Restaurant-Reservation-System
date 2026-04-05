import { Component, Input } from '@angular/core';
import { DishModel } from '../../../models/menu-model';
import { NgClass } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-dish',
  imports: [NgClass],
  templateUrl: './dish.html',
  styleUrl: './dish.scss',
})
export class Dish {
  @Input() dish !: DishModel
}
