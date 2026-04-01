import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestaurantModel } from '../models/restaurant-model';
import { Globals } from './globals';

@Injectable({
  providedIn: 'root',
})
export class RestaurantService {

  constructor(private globals: Globals, private http: HttpClient) {}

  private getAuthHeaders(token: string) {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`
      })
    };
  }

  // 🔹 PUBLIC
  getAll(): Observable<RestaurantModel[]> {
    return this.http.get<RestaurantModel[]>(
      `${this.globals.apiUrl}/Restaurant/GetAll`
    );
  }

  getById(id: number): Observable<RestaurantModel> {
    return this.http.get<RestaurantModel>(
      `${this.globals.apiUrl}/Restaurant/GetById/${id}`
    );
  }

  // 🔹 ADMIN
  add(token: string, data: any): Observable<any> {
    return this.http.post(
      `${this.globals.apiUrl}/Restaurant/Add`,
      data,
      this.getAuthHeaders(token)
    );
  }

  update(token: string, id: number, data: any): Observable<any> {
            console.log(id, data)
    return this.http.put(
      `${this.globals.apiUrl}/Restaurant/Update/${id}`,
      data,
      this.getAuthHeaders(token)
    );
  }

  delete(token: string, id: number): Observable<any> {
    return this.http.delete(
      `${this.globals.apiUrl}/Restaurant/Remove/${id}`,
      this.getAuthHeaders(token)
    );
  }
}