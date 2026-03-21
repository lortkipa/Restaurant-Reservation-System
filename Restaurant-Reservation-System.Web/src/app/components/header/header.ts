import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  blackText:string = "Step"
  orangeText:string = "Academy"

  inputText:string = "Search Anything Here"
}
