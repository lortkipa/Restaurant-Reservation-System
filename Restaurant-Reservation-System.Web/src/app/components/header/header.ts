import { Component, HostListener } from '@angular/core';
import { Home } from '../home/home';
import { RouterLink } from "@angular/router";

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [Home, RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  whiteText: string = "STEP"
  yellowText: string = "ACADEMY"

  isScrolled = false;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.scrollY > 50;
  }
}
