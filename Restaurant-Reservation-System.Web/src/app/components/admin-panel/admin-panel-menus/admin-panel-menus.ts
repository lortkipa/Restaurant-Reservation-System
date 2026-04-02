import { Component, signal, WritableSignal, inject } from '@angular/core';
import { CreateDishModel, DishModel, MenuDishModel } from '../../../models/menu-model';
import { AlertService } from '../../../services/alert-service';
import { MenuService } from '../../../services/menu-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { toSignal } from '@angular/core/rxjs-interop';
import { RestaurantModel } from '../../../models/restaurant-model';
import { RestaurantService } from '../../../services/restaurant-service';

@Component({
  standalone: true,
  selector: 'app-admin-panel-menus',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-menus.html',
  styleUrl: './admin-panel-menus.scss',
})
export class AdminPanelMenus {
  restaurants: WritableSignal<RestaurantModel[]> = signal([]);
  showForm = signal(false);
  editMode = signal(false);

  token: string = localStorage.getItem('token') || '';

  form = signal({
    name: '',
    location: '',
    description: '',
    email: '',
    totalTables: 1,
    seatsPerTable: 1
  });

  constructor(
    private restaurantService: RestaurantService,
    private menuService: MenuService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.load();
  }

  load() {
    this.restaurantService.getAll().subscribe({
      next: (data) => this.restaurants.set(data),
      error: (err) => console.error(err),
    });
  }

  selectedRestaurantId = signal<number>(0);
  selectedRestaurantName = signal<string | null>(null);
  selectedMenuDishes = signal<MenuDishModel[] | null>(null);

  selectRestaurant(id: number, name: string) {
    if (this.selectedRestaurantId() === id) {
      this.selectedRestaurantId.set(0);
      this.selectedMenuDishes.set(null);
      this.selectedRestaurantName.set(null)
    } else {
      this.selectedRestaurantId.set(id);
      this.selectedRestaurantName.set(name)

      this.menuService.GetMenusWithDishes(id).subscribe((data) => {
        if (this.selectedRestaurantId() === id) {
          this.selectedMenuDishes.set(data);
        }
      });
    }
  }

  selectedMenuId = signal<number>(0)
  selectedMenuName = signal<string>('')
  selectedDishes = signal<DishModel[] | null>(null)
  selectMenu(id: number, name: string, dishes: DishModel[]) {
    if (this.selectedMenuId() == id) {
      this.selectedMenuId.set(0)
      this.selectedMenuName.set('')
      this.selectedDishes.set(null)
    } else {
      this.selectedMenuId.set(id)
      this.selectedMenuName.set(name)
      this.selectedDishes.set([...dishes]);
    }
  }

  // Show/hide add/edit dish form
  showDishForm = signal(false);
  dishEditMode = signal(false);

  // Form for adding/editing dish
  dishForm = signal<CreateDishModel>({
    name: '',
    price: 0,
    isAvaiable: true
  });

  // Method to open Add Dish form
  openAddDish() {
    this.dishEditMode.set(false);
    this.dishForm.set({
      name: '',
      price: 0,
      isAvaiable: true
    });
    this.showDishForm.set(true);
  }

  // Method to open Edit Dish form
  selectedDishId = signal<number>(0)
  openEditDish(dish: DishModel) {
    this.dishEditMode.set(true);
    this.dishForm.set({ ...dish }); // copy to avoid mutating original
    this.showDishForm.set(true);
    this.selectedDishId.set(dish.id)
  }

  // Method to save dish
  saveDish() {
    const newDish = this.dishForm();
    if (!newDish.name) { this.alert.error("Failed to Add Dish", "Name is Empty"); return; }
    if (newDish.price == 0) { this.alert.error("Failed to Add Dish", "Price is 0"); return; }
    if (newDish.price < 0) { this.alert.error("Failed to Add Dish", "Price is Less Then 0"); return; }

    if (this.dishEditMode()) {
      this.alert.confirm("Are You Sure?").then((res) => {
        if (!res.isConfirmed) return

        this.menuService.UpdateDish(this.selectedDishId(), this.dishForm()).subscribe({
          next: () => {
            this.alert.success("Dish Updated", '').then(() => this.load());
          },
          error: (err) => this.alert.error("Dish Not Updated", err.error.message)
        })
      })
    } else {
      this.alert.confirm("Are You Sure?").then((res) => {
        if (!res.isConfirmed) return;
        this.menuService.AddDish(this.selectedMenuId(), this.dishForm()).subscribe({
          next: () => {
            this.alert.success("Dish Added", '').then(() => this.load());
          },
          error: (err) => this.alert.error("Dish Not Failed", err.error.message)
        });
      })
    }

    this.showDishForm.set(false);
  }

  removeDish(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.menuService.RemoveDish(id).subscribe({
        next: () => {
          this.alert.success("Dish Removed", '').then(() => this.load());
        },
        error: (err) => this.alert.error("Dish Not Removed", err.error.message)
      })
    })
  }
}