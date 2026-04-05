import { Component } from '@angular/core';
import { RestaurantService } from '../../../services/restaurant-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LocalStorageService } from '../../../services/local-storage-service';
import { Router, RouterLink } from "@angular/router";
import { RestaurantModel } from '../../../models/restaurant-model';
import { AlertService } from '../../../services/alert-service';
import { CreateReservationModel } from '../../../models/reservation-model';
import { ReservationService } from '../../../services/reservation-service';

@Component({
  standalone: true,
  selector: 'app-reservation-form',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './reservation-form.html',
  styleUrl: './reservation-form.scss',
})
export class ReservationForm {
  Request: string = "has been requested. We'll send a confirmation to your email within 24 hours."

  IsLoggedIn: Boolean = false;

  restaurants: any[] = [];
  selectedRestaurantIndex: number | null = null;
  selectedRestaurant: RestaurantModel = {
    id: 0,
    name: '',
    location: '',
    description: '',
    email: '',
    totalTables: 1,
    seatsPerTable: 1
  };
  selectedDate: string = '';
  selectedGuests: number = 1;
  customGuestCount: number | null = null;
  selectedTables: number = 1;

  token: string = ''

  constructor(private router: Router, private alert: AlertService, private restaurantService: RestaurantService, private localStorage: LocalStorageService, private reservationsService: ReservationService) {
    this.token = localStorage.getItem('token')
  }

  ngOnInit(): void {
    this.IsLoggedIn = this.localStorage.getItem('token') != '';
    this.loadRestaurants();
  }

  loadRestaurants() {
    this.restaurantService.getAll().subscribe({
      next: (data) => {
        this.restaurants = data;
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  selectRestaurant(index: number) {
    this.selectedRestaurantIndex = index;
    this.selectedRestaurant = this.restaurants[index];
    this.updateTableCount();
  }
  updateTableCount() {
    let guestCount = this.selectedGuests

    if (guestCount > 0) {
      let calculated = Math.ceil(guestCount / 4);

      if (this.selectedRestaurant) {
        let maxTables = Number(this.selectedRestaurant.totalTables);

        this.selectedTables = calculated > maxTables ? maxTables : calculated;
      } else {
        this.selectedTables = calculated;
      }
    } else {
      this.selectedTables = 1;
    }
  }

  getTablesArray(): number[] {
    if (!this.selectedRestaurant || !this.selectedRestaurant.totalTables) {
      return [1, 2, 3, 4, 5];
    }
    let total = Number(this.selectedRestaurant.totalTables);
    return Array.from({ length: total }, (_, i) => i + 1);
  }

  submitReservation(form: any) {
    let formTitle = "Reservation Failed"

    if (form.invalid) {
      this.alert.error('adfa', "Date is empty");
      if (!this.selectedDate) {
        this.alert.error(formTitle, "Date is empty");
        return;
      }
    }

    const selected = new Date(this.selectedDate);
    const today = new Date();

    selected.setHours(0, 0, 0, 0);
    today.setHours(0, 0, 0, 0);

    if (selected < today) {
      this.alert.error(formTitle, "Date cannot be in the past");
      return;
    }

    this.alert.confirm("Are You Sure?").then((res) => {
      if (res.isConfirmed) {
        this.reservationsService.add(this.token, {
          customerId: 0,
          restaurantId: this.selectedRestaurant.id,
          statusId: 2,
          date: this.selectedDate,
          tableNumber: Number(this.selectedTables),
          guestCount: Number(this.selectedGuests)
        }).subscribe({
          next: () => {
            this.alert.success("Reservation was successful", '').then(() => {
              this.router.navigate(['/home']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Reservation Failed", err.error);
          }
        })
      }
    })
  }

}

