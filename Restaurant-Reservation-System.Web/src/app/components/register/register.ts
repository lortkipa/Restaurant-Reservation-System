import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { CommonModule } from '@angular/common';
import { RegisterModel } from '../../models/user-model';
import { UserService } from '../../services/user-service';
import { AlertService } from '../../services/alert-service';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  constructor(private user: UserService, private alert: AlertService) { }

  confirmPassword: string = '';
  submitted = false;

  registerModel: RegisterModel = {
    person: {
      firstName: '',
      lastName: '',
      phone: '',
      address: ''
    },
    username: '',
    email: '',
    password: '',
    registrationDate: new Date()
  };

  get passwordMismatch(): boolean {
    return this.confirmPassword !== '' && this.confirmPassword !== this.registerModel.password;
  }

  onSubmit(form: any) {
    if (form.invalid) {
      let missingFields = []
      if (!this.registerModel.person.firstName) {this.alert.error("First Name is empty"); return;}
      if (!this.registerModel.person.lastName) {this.alert.error("Last Name is empty"); return;}
      if (!this.registerModel.person.phone) {this.alert.error("Phone is empty"); return;}
      if (!this.registerModel.email) {this.alert.error("Email is empty"); return;}
      if (!this.registerModel.person.address) {this.alert.error("Address is empty"); return;}
      if (!this.registerModel.username) {this.alert.error("Username is empty"); return;}
      if (!this.registerModel.password) {this.alert.error("Password is empty"); return;}

      return;
    }
    this.user.Register(this.registerModel).subscribe({
      next: (res) => {
        this.alert.success("Registration was successful");
      },
      error: (err) => {
        this.alert.error("Something went wrong"); 
      }
    });
  }
}