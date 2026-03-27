import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { CommonModule } from '@angular/common';
import { RegisterModel } from '../../models/user-model';
import { UserService } from '../../services/user-service';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register {
  constructor(private user: UserService) { }

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
    this.submitted = true;

    if (form.invalid || this.passwordMismatch) {
      return;
    }

    this.user.Register(this.registerModel).subscribe({
      next: (res) => {
        console.log('Success', res.message);
      },
      error: (err) => {
        console.log('Error', err.message);
      }
    });
  }
}