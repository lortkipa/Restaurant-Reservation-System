import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd, RouterLink } from '@angular/router';
import { filter } from 'rxjs/operators';
import { CommonModule, NgIf } from '@angular/common';
import { Home } from '../home/home';
import { Globals } from '../../services/globals';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [Home, RouterLink, NgIf, CommonModule],
  templateUrl: './header.html',
  styleUrls: ['./header.scss'],
})
export class Header implements OnInit {
  whiteText: string = "STEP";
  yellowText: string = "ACADEMY";

  pageName: string = '';
  isScrolled: boolean = false;
  isLoggedIn: boolean = false;

  constructor(public globals : Globals, private router: Router) { }

  ngOnInit() {
    // Listen for route changes
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event: NavigationEnd) => {
        const url = event.urlAfterRedirects;
        this.pageName = url.split('/').filter(s => s).pop() || '';
      });
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.scrollY > 50;
  }
}