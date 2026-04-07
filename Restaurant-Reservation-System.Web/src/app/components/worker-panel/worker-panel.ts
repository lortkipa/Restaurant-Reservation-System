import { Component, signal, WritableSignal } from '@angular/core';
import { Globals } from '../../services/globals';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-worker-panel',
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './worker-panel.html',
  styleUrl: './worker-panel.scss',
})
export class WorkerPanel {
  constructor(public globals: Globals) {}

  show: WritableSignal<string> = signal("");
  // Signal to handle mobile menu state
  isSidebarOpen = signal(false);

  switchShow(show: string): void {
    this.show.set(show);
    // Close sidebar automatically on mobile after clicking a link
    this.isSidebarOpen.set(false);
  }

  toggleSidebar(): void {
    this.isSidebarOpen.update(v => !v);
  }
}
