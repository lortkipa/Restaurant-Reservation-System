import { Component, effect, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LocalStorageService } from '../../services/local-storage-service';
import { UserService } from '../../services/user-service';
import { AlertService } from '../../services/alert-service';
import { UserPersonModel } from '../../models/user-model';
import { RoleModel } from '../../models/role-model';
import { ReservationService } from '../../services/reservation-service';
import { ReservationModel } from '../../models/reservation-model';
import { RestaurantService } from '../../services/restaurant-service';
import { forkJoin } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-profile',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './profile.html',
  styleUrls: ['./profile.scss'],
})
export class Profile {
  token = signal<string>('');
  isAdmin = signal(false);
  userName = signal('');
  editAccount = signal(false);
  editPersonal = signal(false);
  password = signal('');

  reservations: ReservationModel[] = [];

  userPerson = signal<UserPersonModel>({
    user: { id: 0, username: '', email: '', registrationDate: new Date() },
    person: { id: 0, firstName: '', lastName: '', phone: '', address: '' }
  });

  constructor(
    private localStorage: LocalStorageService,
    private userService: UserService,
    private router: Router,
    private alert: AlertService,
    private reservationService: ReservationService,
    private restaurantService : RestaurantService
  ) {
    this.token.set(this.localStorage.getItem('token') || '');

    effect(() => {
      let currentToken = this.token();
      if (currentToken) {
        this.userService.getRoles(currentToken).subscribe((roles: RoleModel[]) => {
          this.isAdmin.set(roles.some(r => r.name === 'Admin'));
        });

        this.userService.getProfile(currentToken).subscribe({
          next: (res) => {
            this.userPerson.set(res);
            this.userName.set(res.user.username);
            this.loadUserReservations();
          },
          error: (err) => console.error('Profile fetch error:', err)
        });
      }
    });
  }

  resNames = signal<string[]>([])
  loadUserReservations() {
  const currentToken = this.token();
  if (!currentToken) return;

  this.reservationService.getMyReservations(currentToken).subscribe({
    next: (data: ReservationModel[]) => {
      // Sort reservations by date ascending
      this.reservations = data
        .sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());

      // Create an array of observables for each restaurant fetch
      const restaurantRequests = this.reservations.map(r =>
        this.restaurantService.getById(r.restaurantId)
      );

      // Use forkJoin to run all requests in parallel and preserve order
      forkJoin(restaurantRequests).subscribe(restaurants => {
        // restaurants array matches reservations array order
        this.resNames.set(restaurants.map(res => res.name));
      });
    },
    error: (err) => console.log('Error loading reservations:', err)
  });
}

  cancelReservation(id: number) {
  let currentToken = this.token(); 
  if (!currentToken) return;

  this.alert.confirm("Are you sure you want to cancel this reservation?").then((result) => {
    if (result.isConfirmed) {
      this.reservationService.cancel(currentToken, id).subscribe({
        next: () => {
          this.alert.success("Reservation Canceled Successfully", "").then(() => {
            this.router.navigate(['/profile']).then(() => {
            window.location.reload();
          });
          });
        },
        error: (err) => {
          this.alert.error("Cancellation Failed", err.error.message || "Something went wrong");
          console.error(err);
        }
      });
    }
  });
}

  getStatusLabel(statusId: number): string {
    let labels: Record<number, string> = { 1: 'Pending', 2: 'Confirmed', 3: 'Canceled' };
    return labels[statusId] || 'Unknown';
  }

  getStatusColor(statusId: number): string {
    let colors: Record<number, string> = { 1: '#c8a96a', 2: '#4caf50', 3: '#ff5252' };
    return colors[statusId] || '#999999';
  }

  toggleAccountEdit() { this.editAccount.update(v => !v); }
  togglePersonalEdit() { this.editPersonal.update(v => !v); }

  logout() {
    let currentToken = this.token();
    if (!currentToken) return;

    this.alert.confirm("Are you sure?").then(res => {
      if (res.isConfirmed) {
        this.userService.logout(currentToken);
        this.localStorage.removeItem('token');
        this.router.navigate(['/home']).then(() => window.location.reload());
      }
    });
  }

  updatePersonDetails(form: any) {
    if (form.invalid) {
      this.alert.error("Update Failed", "Please fill all required fields correctly.");
      return;
    }

    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;
      let p = this.userPerson().person;
      this.userService.updatePersonalDetails(this.token(), p).subscribe({
        next: () => this.alert.success("Personal Info Updated", '').then(() => window.location.reload()),
        error: err => this.alert.error("Update Failed", err.error.message)
      });
    });
  }

  updateUserDetails(form: any): void {
    if (form.invalid) {
      let u = this.userPerson().user;
      if (!u.username) this.alert.error("Update Failed", "Username is empty");
      if (!u.email) this.alert.error("Update Failed", "Email is empty");
      if (!this.password()) this.alert.error("Update Failed", "Password is empty");

      return;
    }

    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;
      let u = this.userPerson().user;
      this.userService.updateProfile(this.token(), {
        username: u.username,
        email: u.email,
        password: this.password()
      }).subscribe({
        next: () => this.alert.success("Account Info Updated", '').then(() => {
          this.router.navigate(['/profile']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Update Failed", err.error.message)
      });
    });
  }

  deleteProfile() {
    this.alert.confirm("Are You Sure? This cannot be undone!").then(conf => {
      if (!conf.isConfirmed) return;
      this.userService.deleteProfile(this.token()).subscribe({
        next: () => this.alert.success("Account Deleted Successfully", '').then(() => {
          this.localStorage.removeItem('token');
          this.router.navigate(['/home']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Delete Failed", err.error.message)
      });
    });
  }
}