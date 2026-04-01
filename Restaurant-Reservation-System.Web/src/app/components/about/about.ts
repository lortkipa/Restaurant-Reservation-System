import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-about',
  imports: [],
  templateUrl: './about.html',
  styleUrl: './about.scss',
})
export class About {
  ngAfterViewInit() {
  const reveals = document.querySelectorAll('.reveal');

  const revealOnScroll = () => {
    const windowHeight = window.innerHeight;

    reveals.forEach((el: any) => {
      const top = el.getBoundingClientRect().top;

      if (top < windowHeight - 100) {
        el.classList.add('in');
      }
    });
  };

  window.addEventListener('scroll', revealOnScroll);
  revealOnScroll(); // trigger on load
}
}
