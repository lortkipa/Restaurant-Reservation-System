import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-error',
  imports: [RouterModule],
  templateUrl: './error.html',
  styleUrl: './error.scss',
})
export class Error {}
