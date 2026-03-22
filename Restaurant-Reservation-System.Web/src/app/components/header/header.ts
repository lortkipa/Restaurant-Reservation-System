import { Component } from '@angular/core';
import { Home } from '../home/home';

@Component({
  selector: 'app-header',
  imports: [Home],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {

}
