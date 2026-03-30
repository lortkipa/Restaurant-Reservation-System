import { Component } from '@angular/core';
import { LocalStorageService } from '../../services/local-storage-service';
import { UserService } from '../../services/user-service';
import { UserModel, UserPersonModel } from '../../models/user-model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AlertService } from '../../services/alert-service';
import { FormsModule } from "@angular/forms";
import { RouterUpgradeInitializer } from '@angular/router/upgrade';

@Component({
  standalone: true,
  selector: 'app-profile',
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile {
  constructor(private localStorage: LocalStorageService, private userService: UserService, private router: Router, private alert: AlertService) { }

  token: string = '';

  userName: string = '';

  editAccount: boolean = false;
  editPersonal: boolean = false;

  toggleAccountEdit() {
    this.editAccount = !this.editAccount
  }
  togglePersonalEdit() {
    this.editPersonal = !this.editPersonal
  }

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
  password: string = '';

  ngOnInit() {
    this.token = this.localStorage.getItem('token')

    if (this.token) {
      this.userService.getProfile(this.token).subscribe({
        next: (res) => {
          this.userPerson = res;
          this.userName = this.userPerson.user.username;
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

  updatePersonDetails(form: any) {
    if (form.invalid) {
      let formTitle = "Update Failed"
      if (!this.userPerson.person.firstName) { this.alert.error(formTitle, "First Name is empty"); return; }
      if (!this.userPerson.person.lastName) { this.alert.error(formTitle, "Last Name is empty"); return; }
      if (!this.userPerson.person.phone) { this.alert.error(formTitle, "Phone is empty"); return; }
      if (!this.userPerson.person.address) { this.alert.error(formTitle, "Address is empty"); return; }

      return;
    }

    this.alert.confirm("Are You Sure?").then((confirmed) => {
      if (confirmed.isConfirmed) {
        this.userService.updatePersonalDetails(this.token, {
          firstName: this.userPerson.person.firstName,
          lastName: this.userPerson.person.lastName,
          address: this.userPerson.person.address,
          phone: this.userPerson.person.phone
        }).subscribe({
          next: () => {
            this.alert.success("Personal Info Updated", '').then(() => {
              this.router.navigate(['/profile']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Update Failed", err.error.message);
          }
        })
      }
    })
  }

  updateUserDetails(form: any) {
    if (form.invalid) {
      let formTitle = "Update Failed"
      if (!this.userPerson.user.username) { this.alert.error(formTitle, "Username is empty"); return; }
      if (!this.userPerson.user.email) { this.alert.error(formTitle, "Email is empty"); return; }
      if (!this.password) { this.alert.error(formTitle, "Password is empty"); return; }

      return;
    }
    this.alert.confirm("Are You Sure?").then((confirmed) => {
      if (confirmed.isConfirmed) {
        this.userService.updateProfile(this.token, {
          username: this.userPerson.user.username,
          email: this.userPerson.user.email,
          password: this.password
        }).subscribe({
          next: () => {
            this.alert.success("Account Info Updated", '').then(() => {
              this.router.navigate(['/profile']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Update Failed", err.error.message);
            console.log(err.error.message);
          }
        })
      }
    })
  }

  deleteProfile() {
    this.alert.confirm("Are You Sure?").then((confirmed) => {
      if (confirmed.isConfirmed) {
        this.userService.deleteProfile(this.token).subscribe({
          next: () => {
            this.alert.success("Account Deleted Successfully", '').then(() => {
              this.localStorage.removeItem('token')
              this.router.navigate(['/home']).then(() => {
                window.location.reload();
              });
            })
          },
          error: (err) => {
            this.alert.error("Update Failed", err.error.message);
            console.log(err.error.message);
          }
        })
      }
    })
  }
}
