import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestaurantModel } from '../models/restaurant-model';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {
  apiUrl:string = "https://localhost:7067/api/Restaurant";

   constructor(private http : HttpClient) {}

  getAll() : Observable<RestaurantModel[]> {
    return this.http.get<RestaurantModel[]>(`${this.apiUrl}/GetAll`)
  }
}
