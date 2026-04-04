import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AlertService } from '../../services/alert-service';
import { Email } from '../../services/email-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact',
  imports: [FormsModule, CommonModule],
  templateUrl: './contact.html',
  styleUrl: './contact.scss',
})
export class Contact {

  constructor(private alert: AlertService, private email: Email, private router: Router) {
  }

  contactData = {
    firstName: '',
    lastName: '',
    email: '',
    message: ''
  };



  onSubmit(form: any) {
    if (!this.contactData.firstName) { this.alert.error('Form Not Submited', 'FirstName is empty'); return; }
    if (!this.contactData.lastName) { this.alert.error('Form Not Submited', 'LastName is empty'); return; }
    if (!this.contactData.email) { this.alert.error('Form Not Submited', 'Email is empty'); return; }
    if (!this.contactData.message) { this.alert.error('Form Not Submited', 'Message is empty'); return; }

    this.alert.confirm('Are You Sure?').then((res) => {
      if (!res.isConfirmed) return;

      this.email.sendContact(this.contactData.firstName, this.contactData.lastName, this.contactData.email, this.contactData.message).then((res) => {
        console.log(res.text)
        this.alert.success(`Thanks for your Message`, "we received your message!").then(() => {
          this.router.navigate(['/home']).then(() => window.location.reload())
        })
      })
    })
  }
}

