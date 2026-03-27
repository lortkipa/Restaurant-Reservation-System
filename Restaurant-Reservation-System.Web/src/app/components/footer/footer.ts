import { Component } from '@angular/core';
import { Globals } from '../../services/globals';

@Component({
  standalone: true,
  selector: 'app-footer',
  imports: [],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer {
  constructor(public globals: Globals) { }

  readonly year = new Date().getFullYear();
}
