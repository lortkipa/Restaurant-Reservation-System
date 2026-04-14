import { Component, signal, WritableSignal, inject } from '@angular/core';
import { CreateDishModel, DishModel, MenuDishModel, MenuModel } from '../../../models/menu-model';
import { AlertService } from '../../../services/alert-service';
import { MenuService } from '../../../services/menu-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { toSignal } from '@angular/core/rxjs-interop';
import { RestaurantModel } from '../../../models/restaurant-model';
import { RestaurantService } from '../../../services/restaurant-service';
import { Route, Router } from '@angular/router';
import { single } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-admin-panel-menus',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-menus.html',
  styleUrl: './admin-panel-menus.scss',
})
export class AdminPanelMenus {
  dishPicSelected = signal<boolean>(false)
  dishPic = signal<File | null>(null)
  dishPicUrl = signal<string | null>(null)
  selectPic() {
    this.alert.getPicture("Select Dish Image").then((res) => {
      if (res.isDismissed) return;

      this.dishPicSelected.set(true)

      if (res.isDenied) {
        this.dishPic.set(null)
        return;
      }

      if (res.isConfirmed) {
        const file = res.value as File;

        if (!file) {
          this.alert.error('No Picture Chosen', '');
          return;
        }

        this.dishPic.set(file)

        const pic = this.dishPic();
        this.dishPicUrl.set(file ? URL.createObjectURL(file) : null);
      }
    })
  }

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
    private alert: AlertService,
    private router: Router
  ) { }

  private isDuplicateMenuName(name: string): boolean {
    const normalized = name.trim().toLowerCase();

    const menus = this.selectedMenuDishes(); // menus for selected restaurant
    if (!menus) return false;

    return menus.some(m => {
      // ignore current menu when editing
      if (this.menuEditMode() && m.id === this.selectedMenuId()) return false;

      return m.name.trim().toLowerCase() === normalized;
    });
  }

  private isDuplicateDishName(name: string): boolean {
    const normalized = name.trim().toLowerCase();

    const dishes = this.selectedDishes();
    if (!dishes) return false;

    return dishes.some(d => {
      // ignore current dish when editing
      if (this.dishEditMode() && d.id === this.selectedDishId()) return false;

      return d.name.trim().toLowerCase() === normalized;
    });
  }

  ngOnInit() {
    this.load();
  }

  showAddMenu = signal<boolean>(false)
  toggleAddMenu() {
    this.showMenuForm.set(!this.showMenuForm())
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

      this.menuService.GetMenusWithDishes(id).subscribe({
        next: (data) => {
          if (this.selectedRestaurantId() === id) {
            this.selectedMenuDishes.set(data);
          }
        },
        error: () => { this.selectedMenuDishes.set([]) }
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
  showMenuForm = signal(false)
  menuEditMode = signal(false)
  showDishForm = signal(false);
  dishEditMode = signal(false);

  // Form for adding/editing dish
  menuForm = signal<MenuModel>({
    id: 0,
    restaurantId: 0,
    name: ''
  })
  dishForm = signal<DishModel>({
    id: 0,
    name: '',
    price: 0,
    isAvaiable: true,
    imageUrl: null
  });

  openAddMenu() {
    this.menuEditMode.set(false)
    this.menuForm.set({
      id: 0,
      name: '',
      restaurantId: 0
    })
    this.showMenuForm.set(true);
  }

  openEditMenu(menuDish: MenuDishModel) {
    this.menuEditMode.set(true)
    this.menuForm.set({
      id: 0,
      name: menuDish.name,
      restaurantId: 0
    })
    this.showMenuForm.set(true);
  }

  // Method to open Add Dish form
  openAddDish() {
    this.dishPicSelected.set(false)
    this.dishEditMode.set(false);
    this.dishForm.set({
      id: 0,
      name: '',
      price: 0,
      isAvaiable: true,
      imageUrl: null
    });
    this.showDishForm.set(true);
  }

  // Method to open Edit Dish form
  selectedDishId = signal<number>(0)
  openEditDish(dish: DishModel) {
    this.dishPicSelected.set(false)
    this.dishEditMode.set(true);
    this.dishForm.set({ ...dish }); // copy to avoid mutating original
    this.showDishForm.set(true);
    this.selectedDishId.set(dish.id)
  }

  saveMenu() {
    const newMenu = this.menuForm()
    if (!newMenu.name) { this.alert.error("Failed to Add Menu", "Name is Empty"); return; }

    if (this.isDuplicateMenuName(newMenu.name)) {
      this.alert.error("Duplicate Menu", "Menu with this name already exists");
      return;
    }

    newMenu.restaurantId = this.selectedRestaurantId()

    console.log("menu: " + this.menuEditMode())
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return
      if (this.menuEditMode()) {
        if (!newMenu.name) { this.alert.error('Menu Not Updated', 'Name is Empty'); return; }
        this.menuService.UpdateMenu(this.selectedMenuId(), newMenu).subscribe(
          () => {
            this.alert.success("Menu Updated", '').then(() => this.load()).then(() => {
              this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
            })
          })
      } else {
        this.menuService.AddMenu(newMenu).subscribe({
          next: () => {
            this.alert.success("Menu Added", '').then(() => this.load()).then(() => {
              this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
            })
          },
          error: (err) => this.alert.error("Menu Not Added", err.error.message)
        })
      }
    })
  }

  // Method to save dish
  saveDish() {
    const newDish = this.dishForm();
    if (!newDish.name) { this.alert.error("Failed to Add Dish", "Name is Empty"); return; }
    if (newDish.price == 0) { this.alert.error("Failed to Add Dish", "Price is 0"); return; }
    if (newDish.price < 0) { this.alert.error("Failed to Add Dish", "Price is Less Then 0"); return; }

    if (this.isDuplicateDishName(newDish.name)) {
      this.alert.error("Duplicate Dish", "Dish with this name already exists");
      return;
    }

    if (this.dishEditMode()) {
      this.alert.confirm("Are You Sure?").then((res) => {
        if (!res.isConfirmed) return

        this.menuService.UpdateDish(this.selectedDishId(), this.dishForm()).subscribe({
          next: () => {
            this.menuService.UpdateDishPicture(this.selectedDishId(), this.dishPic()).subscribe({
              next: () => {
                this.alert.success("Dish Updated", '').then(() => this.load()).then(() => {
                  this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
                })
              },
              error: (err) => { this.alert.error("Dish Not Updated", err.error.message) }
            })
          },
          error: (err) => { this.alert.error("Dish Not Updated", err.error.message) }
        })
      })
    } else {
      this.alert.confirm("Are You Sure?").then((res) => {
        if (!res.isConfirmed) return;
        this.menuService.AddDish(this.selectedMenuId(), this.dishForm()).subscribe({
          next: (data) => {
            this.menuService.UpdateDishPicture(data.id, this.dishPic()).subscribe({
              next: () => {
                this.alert.success("Dish Added", '').then(() => this.load()).then(() => {
                  this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
                })
              },
              error: (err) => { this.alert.error("Dish Not Added", err.error.message) }
            })
          },
          error: (err) => this.alert.error("Dish Not Added", err.error.message)
        });
      })
    }

    this.showDishForm.set(false);
  }

  removeMenu(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.menuService.RemoveMenu(id).subscribe({
        next: () => {
          this.alert.success("Menu Removed", '').then(() => this.load()).then(() => {
            this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
          })
        },
        error: (err) => this.alert.error("Menu Not Removed", err.error.message)
      })
    })
  }

  removeDish(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.menuService.RemoveDish(id).subscribe({
        next: () => {
          this.alert.success("Dish Removed", '').then(() => this.load()).then(() => {
            this.router.navigate(['/admin-panel/menus']).then(() => { window.location.reload(); });
          })
        },
        error: (err) => this.alert.error("Dish Not Removed", err.error.message)
      })
    })
  }
}