import { Injectable } from '@angular/core';
import { Globals } from './globals';
import { HttpClient } from '@angular/common/http';
import { TeamMember } from '../models/developer-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DeveloperService {
  constructor(private globals: Globals, private http: HttpClient){}

  GetAll(): Observable<TeamMember[]> {
      return this.http.get<TeamMember[]>(`${this.globals.apiUrl}/Developer/GetAll`)
    }
}
