import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private globals: Globals, private http: HttpClient) { }

  // Register() : Observable<AuthResponseModel> {
  //     return this.http.get<RestaurantModel[]>(`${this.globals.apiUrl}/GetAll`)
  // }
}
