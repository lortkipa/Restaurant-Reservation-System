import { Component, signal, WritableSignal } from '@angular/core';
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
  restaurants: WritableSignal<RestaurantModel[]> = signal([]);
  showForm: WritableSignal<boolean> = signal(false);
  editMode: WritableSignal<boolean> = signal(false);
  selectedId: WritableSignal<number> = signal(0);

  form = signal({
    name: '',
    location: '',
    description: '',
    email: '',
    totalTables: 0,
    seatsPerTable: 0
  });

  constructor(
    private router: Router,
    private restaurantService: RestaurantService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.restaurantService.getAll().subscribe({
      next: (data) => this.restaurants.set(data),
      error: (err) => console.error(err),
    });
  }

  openAdd() {
    this.editMode.set(false);
    this.selectedId.set(0);
    this.form.set({
      name: '',
      location: '',
      description: '',
      email: '',
      totalTables: 0,
      seatsPerTable: 0
    });
    this.showForm.set(true);
  }

  openEdit(r: RestaurantModel) {
    this.editMode.set(true);
    this.selectedId.set(r.id);
    this.form.set({
      name: r.name,
      location: r.location,
      description: r.description,
      email: r.email,
      totalTables: +r.totalTables,
      seatsPerTable: +r.seatsPerTable
    });
    this.showForm.set(true);
  }

  save() {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      const payload = this.form();
      if (this.editMode()) {
        this.restaurantService.update(this.selectedId(), payload).subscribe({
          next: () => {
            this.alert.success("Restaurant Updated", '').then(() => {
              this.load();
              this.showForm.set(false);
            });
          },
          error: (err) => this.alert.error("Restaurant Not Updated", err.error?.message)
        });
      } else {
        this.restaurantService.add(payload).subscribe({
          next: () => {
            this.alert.success("Restaurant Added", '').then(() => {
              this.load();
              this.showForm.set(false);
            });
          },
          error: (err) => this.alert.error("Restaurant Not Added", err.error?.message)
        });
      }
    });
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.restaurantService.delete(id).subscribe({
        next: () => {
          this.alert.success("Restaurant Deleted", '').then(() => this.load());
        },
        error: (err) => this.alert.error("Delete Failed", err.error?.message)
      });
    });
  }
}