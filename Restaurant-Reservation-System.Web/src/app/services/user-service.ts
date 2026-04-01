import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';
import { GoldLine } from '../components/gold-line/gold-line';
import { LoginModel, RegisterModel, UpdatePersonModel, UpdateUserModel, UserModel, UserPersonModel } from '../models/user-model';
import {  RoleModel } from '../models/role-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private globals: Globals, private http: HttpClient) { }

  Register(data: RegisterModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Register`, data);
  }

  Login(data: LoginModel): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Login`, data);
  }

  logout(token: string): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Logout`, Number(token));
  }

  getRoles(token: string): Observable<RoleModel[]> {
    return this.http.get<RoleModel[]>(
      `${this.globals.apiUrl}/User/GetRolesById`,
      { headers: { Authorization: `Bearer ${token}` } }
    );
  }

  getProfile(token: string): Observable<UserPersonModel> {
    return this.http.get<UserPersonModel>(
      `${this.globals.apiUrl}/User/GetProfile`,
      { headers: { Authorization: `Bearer ${token}` } }
    );
  }

  updateProfile(token: string, data: UpdateUserModel): Observable<AuthResponseModel> {
    return this.http.put<AuthResponseModel>(
      `${this.globals.apiUrl}/User/UpdateProfile`,
      data,
      { headers: { Authorization: `Bearer ${token}` } }
    );
  }

  updatePersonalDetails(token: string, data: UpdatePersonModel): Observable<AuthResponseModel> {
    return this.http.put<AuthResponseModel>(
      `${this.globals.apiUrl}/User/UpdatePersonalInfo`,
      data,
      { headers: { Authorization: `Bearer ${token}` } }
    );
  }

  deleteProfile(token: string): Observable<AuthResponseModel> {
    return this.http.delete<AuthResponseModel>(
      `${this.globals.apiUrl}/User/DeleteProfile`,
      { headers: { Authorization: `Bearer ${token}` } }
    );
  }
}
