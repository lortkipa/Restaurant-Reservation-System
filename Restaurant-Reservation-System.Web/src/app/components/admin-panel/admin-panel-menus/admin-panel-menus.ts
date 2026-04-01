import { Component, signal, WritableSignal } from '@angular/core';
import { MenuDishModel } from '../../../models/menu-model';

@Component({
  standalone: true,
  selector: 'app-admin-panel-menus',
  imports: [],
  templateUrl: './admin-panel-menus.html',
  styleUrl: './admin-panel-menus.scss',
})
export class AdminPanelMenus {
  restaurants: WritableSignal<MenuDishModel[]> = signal([]);
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
}
