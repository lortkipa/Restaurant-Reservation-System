import { Component, EventEmitter, Output, output } from '@angular/core';
import { RouterLink } from "@angular/router";
import { LocalStorageService } from '../../../services/local-storage-service';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-hero',
  imports: [RouterLink,  CommonModule],
  templateUrl: './hero.html',
  styleUrl: './hero.scss',
})
export class Hero {
  constructor(private localStorageService : LocalStorageService){
    this.isLoggedIn = this.localStorageService.getItem('token') != ''
    // console.log(this.isLoggedIn)
  }

  isLoggedIn: boolean = false; 

  description:string = "Exceptional restaurants, one seamless reservation. Create a free account and secure your perfect evening."
  
  scrollToRestaurants() {
    const el = document.getElementById('choose');
    el?.scrollIntoView({ behavior: 'smooth' });
  }
}
