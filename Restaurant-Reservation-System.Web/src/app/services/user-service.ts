import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';
import { GoldLine } from '../components/gold-line/gold-line';
import { RegisterModel } from '../models/user-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private globals: Globals, private http: HttpClient) { }

  Register(data : RegisterModel) : Observable<AuthResponseModel> {
      return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/User/Register`, data);
  }
}
