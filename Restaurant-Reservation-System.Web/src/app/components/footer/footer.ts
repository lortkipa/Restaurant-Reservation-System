import { Component } from '@angular/core';
import { Globals } from '../../services/globals';
import { NavigationEnd, Route, Router } from '@angular/router';
import { filter } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-footer',
  imports: [CommonModule],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer {
  constructor(public globals: Globals, private router : Router) { }

  pageName: string = ''
  readonly year = new Date().getFullYear();

  ngOnInit() {
     // Listen for route changes
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        const url = event.urlAfterRedirects;
        this.pageName = url.split('/').filter(s => s).pop() || '';
      });
  }
}
