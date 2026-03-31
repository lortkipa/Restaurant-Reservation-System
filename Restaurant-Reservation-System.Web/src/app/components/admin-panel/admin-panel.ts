import { Component } from '@angular/core';
import { Reservations } from "./reservations/reservations";
import { RouterOutlet, RouterLinkActive, RouterLinkWithHref } from '@angular/router';
import { FormsModule } from "@angular/forms";
import { CommonModule } from '@angular/common';
import { Globals } from '../../services/globals';
import { Restaurants } from '../home/restaurants/restaurants';
import { AdminPanelRestaurants } from "./admin-panel-restaurants/admin-panel-restaurants";

@Component({
  standalone: true,
  selector: 'app-admin-panel',
  imports: [Reservations, FormsModule, CommonModule, RouterLinkActive, Restaurants, AdminPanelRestaurants, RouterOutlet, RouterLinkWithHref],
  templateUrl: './admin-panel.html',
  styleUrl: './admin-panel.scss',
})
export class AdminPanel {
  constructor(public globals : Globals){}

  show: string = "reservations"

  switchShow(show: string) {
    this.show = show;
  }
}
