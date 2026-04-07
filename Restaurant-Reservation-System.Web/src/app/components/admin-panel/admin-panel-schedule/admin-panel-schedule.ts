import { Component, signal } from '@angular/core';
import { UserService } from '../../../services/user-service';
import { LocalStorageService } from '../../../services/local-storage-service';
import { UserModel } from '../../../models/user-model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-admin-panel-schedule',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-schedule.html',
  styleUrl: './admin-panel-schedule.scss',
})
export class AdminPanelSchedule {
  token: string = ''

  workers = signal<UserModel[]>([])
  selectedWorkerIndex = signal<number>(-1)

  constructor(private userService: UserService, private localStorage: LocalStorageService) {
    this.token = this.localStorage.getItem('token')

    this.userService.getAllWorkers(this.token).subscribe((data) => {
      this.workers.set(data);
    })
  }
}
