import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DeveloperService } from '../../services/developer-service';
import { TeamMember } from '../../models/developer-model';
import { RouterLink } from "@angular/router";
export interface DeveloperModel {
  initials: string;
  name: string;
  role: string;
  bio: string;
  github: string;
  linkedin: string;
  portfolio: string;
}
@Component({
  standalone: true,
  selector: 'app-about',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './about.html',
  styleUrl: './about.scss',
})
export class About {
  members = signal<TeamMember[]>([
  {
    id: 1,
    role: 'Team Leader | Full Stack Developer',
    githubLink: 'https://github.com/lortkipa',
    linkedinLink: 'https://www.linkedin.com/in/nikoloz-lortkipanidze-2b4263329/',
    portfolioLink: null,
    person: {
      id: 1,
      firstName: 'Nikoloz',
      lastName: 'Lortkipanidze',
      phone: '577711701',
      address: 'Near Lisi Lake'
    }
  },
  {
    id: 2,
    role: 'Full Stack Developer',
    githubLink: 'https://github.com/temo',
    linkedinLink: 'https://linkedin.com/in/temo',
    portfolioLink: null,
    person: {
      id: 2,
      firstName: 'Temo',
      lastName: 'Totoshvili',
      phone: '577711702',
      address: 'Tbilisi, Saburtalo District'
    }
  },
  {
    id: 3,
    role: 'Backend Developer',
    githubLink: 'https://github.com/davit',
    linkedinLink: 'https://linkedin.com/in/davit',
    portfolioLink: null,
    person: {
      id: 3,
      firstName: 'Davit',
      lastName: 'Papava',
      phone: '577711703',
      address: 'Kutaisi, Central Area'
    }
  },
  {
    id: 4,
    role: 'Full Stack Developer',
    githubLink: 'https://github.com/demetre',
    linkedinLink: 'https://linkedin.com/in/demetre',
    portfolioLink: null,
    person: {
      id: 4,
      firstName: 'Demetre',
      lastName: 'Kvirikashvili',
      phone: '577711704',
      address: 'Batumi, Old Boulevard'
    }
  },
  {
    id: 5,
    role: 'Front End Developer',
    githubLink: null,
    linkedinLink: null,
    portfolioLink: null,
    person: {
      id: 5,
      firstName: 'Saba',
      lastName: 'Dolidze',
      phone: '577711705',
      address: 'Rustavi, City Center'
    }
  }
]);

  constructor(private cdr: ChangeDetectorRef, private devServ: DeveloperService) {
  }

  ngAfterViewInit() {
    const obs = new IntersectionObserver(entries => {
      entries.forEach(e => {
        if (e.isIntersecting) {
          e.target.classList.add('in');
        }
      });
    }, { threshold: 0.1 });

    document.querySelectorAll('.reveal').forEach(el => obs.observe(el));
  }
}
