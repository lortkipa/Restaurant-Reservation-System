import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestaurantModel } from '../models/restaurant-model';
import { Globals } from './globals';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {

   constructor(private globals : Globals, private http : HttpClient) {}

  getAll() : Observable<RestaurantModel[]> {
    return this.http.get<RestaurantModel[]>(`${this.globals.apiUrl}/Restaurant/GetAll`)
  }
}
