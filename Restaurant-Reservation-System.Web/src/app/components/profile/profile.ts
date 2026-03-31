import { Component, effect, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LocalStorageService } from '../../services/local-storage-service';
import { UserService } from '../../services/user-service';
import { AlertService } from '../../services/alert-service';
import { UserPersonModel } from '../../models/user-model';
import { RoleModel } from '../../models/role-model';

@Component({
  standalone: true,
  selector: 'app-profile',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './profile.html',
  styleUrls: ['./profile.scss'],
})
export class Profile {
  // --- Signals for reactive state ---
  token = signal<string>('');
  isAdmin = signal(false);
  userName = signal('');
  editAccount = signal(false);
  editPersonal = signal(false);
  password = signal('');

  userPerson = signal<UserPersonModel>({
    user: { id: 0, username: '', email: '', registrationDate: new Date() },
    person: { id: 0, firstName: '', lastName: '', phone: '', address: '' }
  });

  constructor(
    private localStorage: LocalStorageService,
    private userService: UserService,
    private router: Router,
    private alert: AlertService
  ) {
    // Automatically fetch token
    this.token.set(this.localStorage.getItem('token') || '');

    // Effect: fetch roles when token changes
    effect(() => {
      const currentToken = this.token();
      if (currentToken) {
        this.userService.getRoles(currentToken).subscribe((roles: RoleModel[]) => {
          this.isAdmin.set(roles.some(r => r.name === 'Admin'));
        });

        // Fetch profile data
        this.userService.getProfile(currentToken).subscribe({
          next: (res) => {
            this.userPerson.set(res);
            this.userName.set(res.user.username);
          },
          error: (err) => console.error(err)
        });
      }
    });
  }

  // --- Methods using signals ---
  toggleAccountEdit() { this.editAccount.update(v => !v); }
  togglePersonalEdit() { this.editPersonal.update(v => !v); }

  logout() {
    const token = this.token();
    if (!token) return;

    this.alert.confirm("Are you sure?").then(res => {
      if (res.isConfirmed) {
        this.userService.logout(token);
        this.localStorage.removeItem('token');
        this.router.navigate(['/home']).then(() => window.location.reload());
      }
    });
  }

  updatePersonDetails(form: any) {
    if (form.invalid) {
      const p = this.userPerson();
      if (!p.person.firstName)  this.alert.error("Update Failed", "First Name is empty");
      if (!p.person.lastName)  this.alert.error("Update Failed", "Last Name is empty");
      if (!p.person.phone)  this.alert.error("Update Failed", "Phone is empty");
      if (!p.person.address)  this.alert.error("Update Failed", "Address is empty");
      
      return;
    }

    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;
      const p = this.userPerson();
      this.userService.updatePersonalDetails(this.token(), {
        firstName: p.person.firstName,
        lastName: p.person.lastName,
        address: p.person.address,
        phone: p.person.phone
      }).subscribe({
        next: () => this.alert.success("Personal Info Updated", '').then(() => {
          this.router.navigate(['/profile']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Update Failed", err.error.message)
      });
    });
  }

  updateUserDetails(form: any): void {
    if (form.invalid) {
      const u = this.userPerson().user;
      if (!u.username) this.alert.error("Update Failed", "Username is empty");
      if (!u.email) this.alert.error("Update Failed", "Email is empty");
      if (!this.password()) this.alert.error("Update Failed", "Password is empty");

      return;
    }

    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;
      const u = this.userPerson().user;
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
    this.alert.confirm("Are You Sure?").then(conf => {
      if (!conf.isConfirmed) return;
      this.userService.deleteProfile(this.token()).subscribe({
        next: () => this.alert.success("Account Deleted Successfully", '').then(() => {
          this.localStorage.removeItem('token');
          this.router.navigate(['/home']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("Update Failed", err.error.message)
      });
    });
  }
}