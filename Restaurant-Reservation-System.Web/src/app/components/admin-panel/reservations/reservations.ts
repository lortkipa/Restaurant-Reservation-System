import { Component, signal, WritableSignal } from '@angular/core';
import { ReservationService } from '../../../services/reservation-service';
import { CommonModule } from '@angular/common';
import { ReservationModel } from '../../../models/reservation-model';
import { AlertService } from '../../../services/alert-service';

@Component({
  standalone: true,
  selector: 'app-reservations',
  imports: [CommonModule],
  templateUrl: './reservations.html',
  styleUrl: './reservations.scss',
})
export class Reservations {
  reservations: WritableSignal<ReservationModel[]> = signal([]);

  constructor(
    private reservationService: ReservationService,
    private alert: AlertService
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.reservationService.getAll().subscribe({
      next: (data) => this.reservations.set(data),
      error: (err) => console.error(err),
    });
  }

  cancel(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.reservationService.cancel(id).subscribe({
        next: () => {
          this.alert.success("Reservation Canceled", '').then(() => {
            // Remove cancelled reservation from the signal array
            const updated = this.reservations().filter(r => r.id !== id);
            this.reservations.set(updated);
          });
        },
        error: (err) => this.alert.error("Reservation Not Canceled", err.error?.message)
      });
    });
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (!res.isConfirmed) return;

      this.reservationService.delete(id).subscribe({
        next: () => {
          this.alert.success("Reservation Removed", '').then(() => {
            // Remove deleted reservation from the signal array
            const updated = this.reservations().filter(r => r.id !== id);
            this.reservations.set(updated);
          });
        },
        error: (err) => this.alert.error("Reservation Not Removed", err.error?.message)
      });
    });
  }
}