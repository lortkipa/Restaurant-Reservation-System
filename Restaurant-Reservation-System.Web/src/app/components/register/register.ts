import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { CommonModule } from '@angular/common';
import { RegisterModel } from '../../models/user-model';
import { UserService } from '../../services/user-service';
import { AlertService } from '../../services/alert-service';
import { resetConsumerBeforeComputation } from '@angular/core/primitives/signals';

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
      id: 0,
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

  onSubmit(form: any) {
    if (form.invalid) {
      let formTitle = "Registration Failed"
      if (!this.registerModel.person.firstName) { this.alert.error(formTitle, "First Name is empty"); return; }
      if (!this.registerModel.person.lastName) { this.alert.error(formTitle, "Last Name is empty"); return; }
      if (!this.registerModel.person.phone) { this.alert.error(formTitle, "Phone is empty"); return; }
      if (!this.registerModel.email) { this.alert.error(formTitle, "Email is empty"); return; }
      if (!this.registerModel.person.address) { this.alert.error(formTitle, "Address is empty"); return; }
      if (!this.registerModel.username) { this.alert.error(formTitle, "Username is empty"); return; }
      if (!this.registerModel.password) { this.alert.error(formTitle, "Password is empty"); return; }

      return;
    }
    this.user.Register(this.registerModel).subscribe({
      next: (res) => {
        this.alert.success("Registration was successful", '');
        console.log(res.message);
      },
      error: (err) => {
        this.alert.confirm("Are you sure?").then((res) => {
          this.alert.error("Registration Failed", err.error.message);
          console.log(err.error.message);
        })
      }
    });
  }
}