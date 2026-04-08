import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';
import { GoldLine } from '../components/gold-line/gold-line';
import { LoginModel, RegisterModel, UpdatePersonModel, UpdateUserModel, UserModel, UserPersonModel } from '../models/user-model';
import { RoleModel, Roles } from '../models/role-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private globals: Globals, private http: HttpClient) { }

  GetAll(token: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.globals.apiUrl}/Admin/GetAllUsers`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    )
  }

  getAllWorkers(token: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.globals.apiUrl}/Admin/GetAllWorkers`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    )
  }

  GetAllCustomers(token: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(`${this.globals.apiUrl}/Admin/GetAllCustomers`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    )
  }

  UpdateUserProfile(token: string, id: number, data: UpdateUserModel): Observable<boolean> {
    return this.http.put<boolean>(`${this.globals.apiUrl}/Admin/UpdateUserProfile/${id}`,
      data,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  removeUserProfile(token: string, id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.globals.apiUrl}/Admin/DeleteUserProfile/${id}`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  setUserRole(id: number, roleId: Roles): Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/Admin/SetRole/${id}/${roleId}`, {})
  }

  removeUserRole(id: number, roleId: Roles): Observable<AuthResponseModel> {
    return this.http.delete<AuthResponseModel>(`${this.globals.apiUrl}/Admin/RemoveRole/${id}/${roleId}`)
  }

  getUserRoles(id: number): Observable<RoleModel[]> {
    return this.http.get<RoleModel[]>(`${this.globals.apiUrl}/Admin/GetRoles/${id}`)
  }

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
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  getProfile(token: string): Observable<UserPersonModel> {
    return this.http.get<UserPersonModel>(
      `${this.globals.apiUrl}/User/GetProfile`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  updateProfile(token: string, data: UpdateUserModel): Observable<AuthResponseModel> {
    return this.http.put<AuthResponseModel>(
      `${this.globals.apiUrl}/User/UpdateProfile`,
      data,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  updatePersonalDetails(token: string, data: UpdatePersonModel): Observable<AuthResponseModel> {
    return this.http.put<AuthResponseModel>(
      `${this.globals.apiUrl}/User/UpdatePersonalInfo`,
      data,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  deleteProfile(token: string): Observable<AuthResponseModel> {
    return this.http.delete<AuthResponseModel>(
      `${this.globals.apiUrl}/User/DeleteProfile`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }

  updateProfilePicture(token: string, data: File | null): Observable<string> {
    const formData = new FormData();

    if (data) {
      formData.append('file', data);
    }

    return this.http.post<string>(
      `${this.globals.apiUrl}/User/UpdateProfilePicture`,
      formData,
      {
        headers: {
          Authorization: `Bearer ${token.replace(/^"|"$/g, '')}`
        }
      }
    );
  }
}
