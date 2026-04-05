import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateReservationModel, ReservationModel } from '../models/reservation-model';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {

  constructor(private globals: Globals, private http: HttpClient) {}

  // 🔹 helper to attach token
  private getAuthHeaders(token: string): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${token.replace(/^"|"$/g, '')}`
    });
  }

  // 🔹 ADMIN ONLY
  getAll(token: string): Observable<ReservationModel[]> {
    return this.http.get<ReservationModel[]>(
      `${this.globals.apiUrl}/Reservation/GetAll`,
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }

  // 🔹 CURRENT USER
  getMyReservations(token: string): Observable<ReservationModel[]> {
    return this.http.get<ReservationModel[]>(
      `${this.globals.apiUrl}/Reservation/Get`,
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }

  // 🔹 CUSTOMER
  add(token: string, data: CreateReservationModel): Observable<ReservationModel> {
    return this.http.post<ReservationModel>(
      `${this.globals.apiUrl}/Reservation/Add`,
      data,
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }

  // 🔹 CUSTOMER (own) or ADMIN
  cancel(token: string, id: number): Observable<any> {
    return this.http.put(
      `${this.globals.apiUrl}/Reservation/Cancel/${id}`,
      {},
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }

  // 🔹 ADMIN ONLY
  updateStatus(token: string, id: number, status: any): Observable<any> {
    return this.http.put(
      `${this.globals.apiUrl}/Reservation/UpdateStatus/${id}?statusId=${status}`,
      {},
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }

  // 🔹 OWNER or ADMIN
  delete(token: string, id: number): Observable<any> {
    return this.http.delete(
      `${this.globals.apiUrl}/Reservation/DeleteReservation/${id}`,
      { headers: this.getAuthHeaders(token.replace(/^"|"$/g, '')) }
    );
  }
}