import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from "@angular/router";
import { LoginModel } from '../../models/user-model';
import { AlertService } from '../../services/alert-service';
import { UserService } from '../../services/user-service';
import { LocalStorageService } from '../../services/local-storage-service';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [RouterLink, CommonModule, RouterLink, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  constructor(private alert: AlertService, private localStorage: LocalStorageService, private user: UserService, private router: Router) { }

  loginModel: LoginModel = {
    username: '',
    password: ''
  };

  onSubmit(form: any) {
    if (form.invalid) {
      let missingFields = []
      let formTitle = "Login Failed"
      if (!this.loginModel.username) { this.alert.error(formTitle, "Username is empty"); return; }
      if (!this.loginModel.password) { this.alert.error(formTitle, "Password is empty"); return; }
      console.log()

      return;
    }

    this.user.Login(this.loginModel).subscribe({
      next: (res) => {
        this.localStorage.setItem("token", res.message);
        this.alert.success("Registration was successful", '').then(() => {
          this.router.navigate(['/home']).then(() => {
            window.location.reload();
          });
        })
      },
      error: (err) => {
        this.alert.error("Registration Failed", err.error.message);
      }
    });

    this.loginModel = {
      username: '',
      password: ''
    };
  }
}
