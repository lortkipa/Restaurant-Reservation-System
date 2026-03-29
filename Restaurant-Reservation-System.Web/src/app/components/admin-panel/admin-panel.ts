import { Component } from '@angular/core';
import { Reservations } from "./reservations/reservations";
import { RouterOutlet, RouterLinkActive } from '@angular/router';
import { FormsModule } from "@angular/forms";
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-admin-panel',
  imports: [Reservations, FormsModule, CommonModule, RouterLinkActive],
  templateUrl: './admin-panel.html',
  styleUrl: './admin-panel.scss',
})
export class AdminPanel {
  show: string = "reservations"

  switchShow(show: string) {
    this.show = show;
  }
}
