import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { MenuDishModel } from '../models/menu-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class MenuService {
  constructor(private globals: Globals, private http: HttpClient) { }

  getAllWithDishes(): Observable<MenuDishModel[]> {
    return this.http.get<MenuDishModel[]>(`${this.globals.apiUrl}/Menu/GetAllWithDishes`);
  }

  GetMenusWithDishes(restaurantId:number): Observable<MenuDishModel[]> {
    return this.http.get<MenuDishModel[]>(`${this.globals.apiUrl}/Menu/GetMenusWithDishes/${restaurantId}`)
  }
}
