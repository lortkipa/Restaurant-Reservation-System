import { ChangeDetectorRef, Component } from '@angular/core';
import { ReservationService } from '../../../services/reservation-service';
import { CommonModule } from '@angular/common';
import { ReservationModel } from '../../../models/reservation-model';
import { AlertService } from '../../../services/alert-service';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-reservations',
  imports: [CommonModule],
  templateUrl: './reservations.html',
  styleUrl: './reservations.scss',
})
export class Reservations {
  reservations: ReservationModel[] = []

  constructor(private reservationService: ReservationService, private alert: AlertService, private router: Router) { }

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
    this.alert.confirm("Are You Sure?").then((res) => {
      if (res.isConfirmed) {
        this.reservationService.cancel(id).subscribe({
          next: () => {
            this.alert.success("Reservation Canceled", '').then(() => {
              this.router.navigate(['/admin-panel']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Reservation Not Canceled", err.error.message);
          }
        });
      }
    })
  }

  delete(id: number) {
    this.alert.confirm("Are You Sure?").then((res) => {
      if (res.isConfirmed) {
        this.reservationService.delete(id).subscribe({
          next: () => {
            this.alert.success("Reservation Removed", '').then(() => {
              this.router.navigate(['/admin-panel']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Reservation Not Removed", err.error.message);
          }
        });
      }
    })
  }
}
