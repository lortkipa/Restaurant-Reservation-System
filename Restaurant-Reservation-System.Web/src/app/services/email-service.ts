import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import emailjs, { type EmailJSResponseStatus } from '@emailjs/browser'
import { Observable } from 'rxjs';
import { EmailJSModel } from '../models/emailjs-model';
import { Globals } from './globals';

@Injectable({
  providedIn: 'root',
})

export class Email {

  constructor(private globals: Globals, private http: HttpClient) { }

  sendContact(firstName: string, lastName: string, email: string, message: string, data: EmailJSModel) {
    if (data.publicKey != null && data.templateId != null && data.serviceId != null) {
      return emailjs.send(data.serviceId, data.templateId, {
        firstName: firstName,
        lastName: lastName,
        email: email,
        message: message
      }, { publicKey: data.publicKey }
      )
    }

    return true;
  }

  getAll(token: string): Observable<EmailJSModel[]> {
    return this.http.get<EmailJSModel[]>(`${this.globals.apiUrl}/EmailJS/GetAll`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    )
  }

  get(token: string): Observable<EmailJSModel> {
    return this.http.get<EmailJSModel>(`${this.globals.apiUrl}/EmailJS/Get`,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    )
  }

  update(token: string, data: EmailJSModel): Observable<boolean> {
    return this.http.put<boolean>(
      `${this.globals.apiUrl}/EmailJS/Update`,
      data,
      { headers: { Authorization: `Bearer ${token.replace(/^"|"$/g, '')}` } }
    );
  }
}
