import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-reservation-form',
  imports: [],
  templateUrl: './reservation-form.html',
  styleUrl: './reservation-form.scss',
})
export class ReservationForm {
    Request : string = "has been requested. We'll send a confirmation to your email within 24 hours."
}
