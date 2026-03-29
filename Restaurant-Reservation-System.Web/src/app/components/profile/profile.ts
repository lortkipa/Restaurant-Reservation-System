import { Component } from '@angular/core';
import { LocalStorageService } from '../../services/local-storage-service';
import { UserService } from '../../services/user-service';
import { UserModel, UserPersonModel } from '../../models/user-model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AlertService } from '../../services/alert-service';

@Component({
  standalone: true,
  selector: 'app-profile',
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile {
  constructor(private localStorage: LocalStorageService, private userService: UserService, private router: Router, private alert: AlertService) { }

  userPerson: UserPersonModel = {
    user: {
      id: 0,
      username: '',
      email: '',
      registrationDate: new Date()
    },
    person: {
      id: 0,
      firstName: '',
      lastName: '',
      phone: '',
      address: ''
    }
  };

  ngOnInit() {
    const token = this.localStorage.getItem('token')

    if (token) {
      this.userService.getProfile(token).subscribe({
        next: (res) => {
          this.userPerson = res;
        },
        error: (err) => {
          console.error(err)
        }
      })
    }
  }

  logout() {
    const token = this.localStorage.getItem('token')

    if (token) {
      this.alert.confirm("Are you sure?").then((res) => {
        if (res.isConfirmed) {
          this.userService.logout(token);
          this.localStorage.removeItem('token')
          this.router.navigate(['/home']).then(() => {
            window.location.reload();
          });
        }
      })
    }
  }
}
