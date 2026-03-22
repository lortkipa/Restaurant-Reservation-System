import { Component, HostListener } from '@angular/core';
import { Home } from '../home/home';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [Home],
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
