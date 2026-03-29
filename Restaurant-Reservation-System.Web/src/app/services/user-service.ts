import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';
import { GoldLine } from '../components/gold-line/gold-line';
import { LoginModel, RegisterModel, UserModel, UserPersonModel } from '../models/user-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private globals: Globals, private http: HttpClient) { }

  getProfile(token: string): Observable<UserPersonModel> {
    return this.http.get<UserPersonModel>(`${this.globals.apiUrl}/user/GetProfile/${token}`);
  }

  Register(data: RegisterModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Register`, data);
  }

  Login(data: LoginModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Login`, data);
  }

  logout(token: string) : Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}User/Logout`, Number(token));
  }
}
