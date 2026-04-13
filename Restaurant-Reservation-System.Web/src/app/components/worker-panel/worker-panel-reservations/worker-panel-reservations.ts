import { CommonModule } from '@angular/common';
import { Component, signal, WritableSignal } from '@angular/core';
import { ReservationModel } from '../../../models/reservation-model';
import { ReservationService } from '../../../services/reservation-service';
import { RestaurantService } from '../../../services/restaurant-service';
import { UserService } from '../../../services/user-service';
import { LocalStorageService } from '../../../services/local-storage-service';
import { AlertService } from '../../../services/alert-service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-worker-panel-reservations',
  imports: [CommonModule, FormsModule],
  templateUrl: './worker-panel-reservations.html',
  styleUrl: './worker-panel-reservations.scss',
})
export class WorkerPanelReservations {
  selectedDate = signal<string>('');

  formatDate(date: any): string {
    const d = new Date(date);
    return d.toISOString().split('T')[0];
  }


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
            this.router.navigate(['/worker-panel/reservations']).then(() => {
              window.location.reload();
            });
          });
        },
        error: (err) => this.alert.error("Reservation Not Canceled", err.error?.message)
      });
    });
  }

  delete(id: number) {
    this.alert.error("Cannot Delete Reservation", "Only Admin Can")
  }
}
