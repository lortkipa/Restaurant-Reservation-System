
// @Component({
//   standalone: true,
//   selector: 'app-reservations',
//   imports: [CommonModule],
//   templateUrl: './reservations.html',
//   styleUrl: './reservations.scss',
// })
// export class Reservations {
//   reservations: WritableSignal<ReservationModel[]> = signal([]);
//   token: string = ''

//   constructor(
//     private reservationService: ReservationService,
//     private alert: AlertService,
//     private localStorage: LocalStorageService
//   ) {
//     this.token = localStorage.getItem('token')
//   }

//   ngOnInit() {
//     this.load();
//   }

//   load() {
//     this.reservationService.getAll(this.token).subscribe({
//       next: (data) => this.reservations.set(data),
//       error: (err) => console.error(err),
//     });
//   }

//   cancel(id: number) {
//     this.alert.confirm("Are You Sure?").then((res) => {
//       if (!res.isConfirmed) return;

//       this.reservationService.cancel(this.token, id).subscribe({
//         next: () => {
//           this.alert.success("Reservation Canceled", '').then(() => {
//             // Remove cancelled reservation from the signal array
//             const updated = this.reservations().filter(r => r.id !== id);
//             this.reservations.set(updated);
//           });
//         },
//         error: (err) => this.alert.error("Reservation Not Canceled", err.error?.message)
//       });
//     });
//   }

//   delete(id: number) {
//     this.alert.confirm("Are You Sure?").then((res) => {
//       if (!res.isConfirmed) return;

//       this.reservationService.delete(this.token, id).subscribe({
//         next: () => {
//           this.alert.success("Reservation Removed", '').then(() => {
//             // Remove deleted reservation from the signal array
//             const updated = this.reservations().filter(r => r.id !== id);
//             this.reservations.set(updated);
//           });
//         },
//         error: (err) => this.alert.error("Reservation Not Removed", err.error?.message)
//       });
//     });
//   }
// }import { Component, signal, WritableSignal, OnInit } from '@angular/core';
import { Component, signal, WritableSignal, OnInit } from '@angular/core';
import { ReservationService } from '../../../services/reservation-service';
import { CommonModule } from '@angular/common';
import { ReservationModel } from '../../../models/reservation-model';
import { AlertService } from '../../../services/alert-service';
import { LocalStorageService } from '../../../services/local-storage-service';
import { UserService } from '../../../services/user-service';
import { RestaurantService } from '../../../services/restaurant-service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-reservations',
  imports: [CommonModule],
  templateUrl: './reservations.html',
  styleUrl: './reservations.scss',
})
export class Reservations implements OnInit {
  reservations: WritableSignal<ReservationModel[]> = signal([]);
  users = signal<any[]>([]);
  restaurants = signal<any[]>([]);
  token: string = ''

  constructor(
    private reservationService: ReservationService,
    private userService: UserService,
    private restaurantService: RestaurantService,
    private alert: AlertService,
    private localStorage: LocalStorageService,
    private router: Router
  ) {
    this.token = this.localStorage.getItem('token') ?? '';
  }

  ngOnInit() {
    this.load();
    this.loadLookups();
  }

  load() {
    this.reservationService.getAll(this.token).subscribe({
      next: (data) => this.reservations.set(data),
      error: (err) => console.error(err),
    });
  }

  loadLookups() {
    this.userService.GetAll(this.token).subscribe(data => this.users.set(data));
    this.restaurantService.getAll().subscribe(data => this.restaurants.set(data));
  }


  getCustomerName(id: number): string {
    const user = this.users().find(u => u.id === id);
    return user ? user.username : `User #${id}`;
  }

  getRestaurantName(id: number): string {
    const rest = this.restaurants().find(r => r.id === id);
    return rest ? rest.name : `Rest #${id}`;
  }


  cancel(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.reservationService.cancel(this.token, id).subscribe({
        next: () => {
          this.alert.success("Reservation Canceled", '').then(() => {
            this.router.navigate(['/admin-panel/reservations']).then(() => {
              window.location.reload();
            });
          });
        },
        error: (err) => this.alert.error("Reservation Not Canceled", err.error?.message)
      });
    });
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.reservationService.delete(this.token, id).subscribe({
        next: () => {
          this.alert.success("Reservation Removed", '').then(() => {
            this.router.navigate(['/admin-panel/reservations']).then(() => {
              window.location.reload();
            });
          });
        },
        error: (err) => this.alert.error("Reservation Not Removed", err.error?.message)
      });
    });
  }
}