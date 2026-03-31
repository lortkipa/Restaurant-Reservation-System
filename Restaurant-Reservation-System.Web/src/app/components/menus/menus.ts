import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Menu } from './menu/menu';
import { MenuDishModel } from '../../models/menu-model';
import { MenuService } from '../../services/menu-service';
import { toSignal } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-menus',
  imports: [Menu, CommonModule],
  templateUrl: './menus.html',
  styleUrls: ['./menus.scss'],
})
export class Menus {
  restaurantId: number = 0

  // Declare signal type only
  menuDishesSignal: any;

  constructor(private menuService: MenuService, private route : ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.restaurantId = params['restaurantId'] ? + params['restaurantId'] : 0;
      console.log('Restaurant ID:', this.restaurantId);
    });

    // Initialize the signal here, AFTER the service is available
    this.menuDishesSignal = toSignal(this.menuService.GetMenusWithDishes(this.restaurantId), { initialValue: [] });
  }
}