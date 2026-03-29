import { Component } from '@angular/core';
import { ReservationService } from '../../../services/reservation-service';
import { CommonModule } from '@angular/common';
import { ReservationModel } from '../../../models/reservation-model';

@Component({
  standalone: true,
  selector: 'app-reservations',
  imports: [CommonModule],
  templateUrl: './reservations.html',
  styleUrl: './reservations.scss',
})
export class Reservations {
  reservations: any[] = []

  constructor(private reservationService: ReservationService) { }

  ngOnInit() {
    this.load()
  }

  load() {
    this.reservationService.getAll().subscribe(data => {
      this.reservations = data
      console.log(data)
    });
  }

  cancel(id: number) {
    this.reservationService.cancel(id).subscribe({
      next: () => {
        this.load();
        const reservation = this.reservations.find(r => r.id === id);
        if (reservation) {
          reservation.statusId = 3;
        }
      }
    });
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.reservationService.delete(id).subscribe({
        next: () => {
          this.load();
          this.reservations = this.reservations.filter(r => r.id !== id);
        }
      });
    }
  }
}
