import { Component } from '@angular/core';
import { Menu } from "./menu/menu";
import { MenuDishModel, MenuModel } from '../../models/menu-model';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-menus',
  imports: [Menu, CommonModule],
  templateUrl: './menus.html',
  styleUrl: './menus.scss',
})
export class Menus {
  menuDishes: MenuDishModel[] = [
    {
      id: 1,
      restaurantId: 1,
      name: "Foods",
      dishes: [
        {
          id: 1,
          name: 'burger',
          price: 9.99,
          isAvailable: true
        },
        {
          id: 2,
          name: 'pizza',
          price: 12,
          isAvailable: true
        }
      ]
    },
    {
      id: 2,
      restaurantId: 1,
      name: "Drinks",
      dishes: [
        {
          id: 3,
          name: 'coca-cola',
          price: 1.85,
          isAvailable: true
        }
      ]
    }
  ]
}
