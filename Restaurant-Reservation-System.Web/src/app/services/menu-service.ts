import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { CreateDishModel, DishModel, MenuDishModel } from '../models/menu-model';
import { Observable } from 'rxjs';
import { AuthResponseModel } from '../models/auth-response-model';

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

  AddDish(menuId: number, data: CreateDishModel) : Observable<AuthResponseModel> {
    return this.http.post<AuthResponseModel>(`${this.globals.apiUrl}/Menu/AddDish/${menuId}`, data)
  }

  UpdateDish(dishId: number, data: CreateDishModel) : Observable<AuthResponseModel> {
    return this.http.put<AuthResponseModel>(`${this.globals.apiUrl}/Menu/UpdateDish/${dishId}`, data)
  }

  RemoveDish(dishId: number) : Observable<AuthResponseModel> {
    return this.http.delete<AuthResponseModel>(`${this.globals.apiUrl}/Menu/RemoveDish/${dishId}`)
  }
}
