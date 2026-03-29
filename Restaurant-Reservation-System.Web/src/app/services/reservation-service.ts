import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ReservationModel } from '../models/reservation-model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {
   constructor(private globals: Globals, private http: HttpClient) {}

  getAll(): Observable<ReservationModel[]> {
    return this.http.get<any[]>(`${this.globals.apiUrl}/Reservation/GetAll`);
  }

  cancel(id: number): Observable<any> {
    return this.http.put(`${this.globals.apiUrl}/Reservation/Cancel/${id}`, {});
  }

  updateStatus(id: number, status: any): Observable<any> {
    return this.http.put(`${this.globals.apiUrl}/Reservation/UpdateStatus/${id}`, status);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.globals.apiUrl}/Reservation/DeleteReservation/${id}`);
  }
}
