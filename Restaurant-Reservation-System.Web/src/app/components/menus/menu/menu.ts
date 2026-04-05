import { Component, Input } from '@angular/core';
import { MenuDishModel, MenuModel } from '../../../models/menu-model';
import { Dish } from "../dish/dish";
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-menu',
  imports: [Dish, CommonModule],
  templateUrl: './menu.html',
  styleUrl: './menu.scss',
})
export class Menu {
  @Input() menuDish !: MenuDishModel

  totalPrice(): number {
    return this.menuDish?.dishes
      ?.filter(dish => dish.isAvaiable) // only include available dishes
      .reduce((sum, dish) => sum + (dish.price || 0), 0) || 0;
  }
}
