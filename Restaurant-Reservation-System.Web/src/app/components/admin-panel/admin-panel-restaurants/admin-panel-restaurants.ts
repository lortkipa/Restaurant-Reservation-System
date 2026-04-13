import { Component, signal, WritableSignal } from '@angular/core';
import { RestaurantModel } from '../../../models/restaurant-model';
import { RestaurantService } from '../../../services/restaurant-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AlertService } from '../../../services/alert-service';
import { RouterLink } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-admin-panel-restaurants',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './admin-panel-restaurants.html',
  styleUrl: './admin-panel-restaurants.scss',
})
export class AdminPanelRestaurants {
  restaurants: WritableSignal<RestaurantModel[]> = signal([]);
  showForm = signal(false);
  editMode = signal(false);
  selectedId = signal(0);

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
    private alert: AlertService
  ) { }

  private isDuplicateName(name: string): boolean {
    const normalized = name.trim().toLowerCase();

    return this.restaurants().some(r => {
      // ignore current item when editing
      if (this.editMode() && r.id === this.selectedId()) return false;

      return r.name.trim().toLowerCase() === normalized;
    });
  }

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
    this.resetForm();
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
    const payload = this.form();

    if (!payload.name) { this.alert.error("Validation", "Name required"); return; }
    if (!payload.location) { this.alert.error("Validation", "Location required"); return; }
    if (!payload.email) { this.alert.error("Validation", "Email required"); return; }
    if (!payload.description) { this.alert.error("Validation", "Description required"); return; }

    if (payload.totalTables <= 0 || payload.seatsPerTable <= 0) {
      this.alert.error("Validation", "Tables must be greater than 0");
      return;
    }

    // ✅ DUPLICATE CHECK HERE
    if (this.isDuplicateName(payload.name)) {
      this.alert.error("Duplicate Name", "Restaurant with this name already exists");
      return;
    }

    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      if (this.editMode()) {
        this.restaurantService.update(this.token, this.selectedId(), payload).subscribe({
          next: () => {
            this.alert.success("Restaurant Updated", '').then(() => {
              this.load();
              this.showForm.set(false);
              this.resetForm();
            });
          },
          error: () => this.alert.error("Update Failed", "Something went wrong")
        });
      } else {
        this.restaurantService.add(this.token, payload).subscribe({
          next: () => {
            this.alert.success("Restaurant Added", '').then(() => {
              this.load();
              this.showForm.set(false);
              this.resetForm();
            });
          },
          error: () => this.alert.error("Add Failed", "Something went wrong")
        });
      }
    });
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.restaurantService.delete(this.token, id).subscribe({
        next: () => {
          this.alert.success("Deleted", '').then(() => this.load());
        },
        error: (err) => this.alert.error("Delete Failed", err.error?.message)
      });
    });
  }

  resetForm() {
    this.form.set({
      name: '',
      location: '',
      description: '',
      email: '',
      totalTables: 1,
      seatsPerTable: 1
    });
  }
}