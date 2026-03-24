import { Component, HostListener } from '@angular/core';
import { Home } from '../home/home';
import { RouterLink } from "@angular/router";
import { CommonModule, NgIf } from '@angular/common';


@Component({
  standalone: true,
  selector: 'app-header',
  imports: [Home, RouterLink, NgIf, CommonModule],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  whiteText: string = "STEP"
  yellowText: string = "ACADEMY"

  isScrolled:boolean = false;
  isLoggedIn:boolean = false;

  @HostListener('window:scroll', [])
  onWindowScroll() {
    this.isScrolled = window.scrollY > 50;
  }
}
