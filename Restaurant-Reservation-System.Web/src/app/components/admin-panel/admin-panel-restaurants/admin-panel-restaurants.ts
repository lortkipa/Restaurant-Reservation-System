import { Component } from '@angular/core';
import { RestaurantModel } from '../../../models/restaurant-model';
import { RestaurantService } from '../../../services/restaurant-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AlertService } from '../../../services/alert-service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-admin-panel-restaurants',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-restaurants.html',
  styleUrl: './admin-panel-restaurants.scss',
})
export class AdminPanelRestaurants {
  restaurants: RestaurantModel[] = [];
  showForm = false;
  editMode = false;
  selectedId = 0;

  form = {
    name: '',
    location: '',
    description: '',
    email: '',
    totalTables: 0,
    seatsPerTable: 0
  };

  constructor(
    private router: Router,
    private restaurantService: RestaurantService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    this.load()
  }

  load() {
    this.restaurantService.getAll().subscribe({
      next: (data) => (this.restaurants = data),
      error: (err) => console.error(err),
    });
  }

  openAdd() {
    this.editMode = false;
    this.selectedId = 0;
    this.form = {
      name: '',
      location: '',
      description: '',
      email: '',
      totalTables: 0,
      seatsPerTable: 0
    };
    this.showForm = true;
  }

  openEdit(r: RestaurantModel) {
    this.editMode = true;
    this.selectedId = r.id;
    this.form = {
      name: r.name,
      location: r.location,
      description: r.description,
      email: r.email,
      totalTables: +r.totalTables,
      seatsPerTable: +r.seatsPerTable
    };
    this.showForm = true;
  }

  save() {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      if (this.editMode) {
        // ✅ UPDATE
        this.restaurantService.update(this.selectedId, this.form).subscribe({
          next: () => {
            this.alert.success("Restaurant Updated", '').then(() => {
              this.load();           // reload data
              this.showForm = false; // close form
            });
          },
          error: (err) => {
            this.alert.error("Restaurant Not Updated", err.error?.message);
          }
        });

      } else {
        // ✅ ADD (FIXED — no update call after)
        this.restaurantService.add(this.form).subscribe({
          next: () => {
            this.alert.success("Restaurant Added", '').then(() => {
              this.load();           // reload data
              this.showForm = false; // close form
            });
          },
          error: (err) => {
            this.alert.error("Restaurant Not Added", err.error?.message);
          }
        });
      }
    });
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.restaurantService.delete(id).subscribe({
        next: () => {
          this.alert.success("Restaurant Deleted", '').then(() => {
            this.load();
          });
        },
        error: (err) => {
          this.alert.error("Delete Failed", err.error?.message);
        }
      });
    });
  }
}