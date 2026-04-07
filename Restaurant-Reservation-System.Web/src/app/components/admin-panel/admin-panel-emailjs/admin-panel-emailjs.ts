import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EmailJSModel } from '../../../models/emailjs-model';
import { EmailJSResponseStatus } from '@emailjs/browser';
import { Email } from '../../../services/email-service';
import { LocalStorageService } from '../../../services/local-storage-service';
import { AlertService } from '../../../services/alert-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-panel-emailjs',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-panel-emailjs.html',
  styleUrl: './admin-panel-emailjs.scss',
})
export class AdminPanelEmailjs {

  templateExample = `You have received a new message from your website contact form.

Name: {{firstName}} {{lastName}}
Email: {{email}}

--------------------------------------------------------------------------------------

Message:
{{message}}`;

  serviceId = signal<string | null>(null)
  templateId = signal<string | null>(null)
  publicKey = signal<string | null>(null)

  token = signal<string>('')

  isSettingSet = signal<boolean>(false)
  constructor(private router: Router, private email: Email, private localStorage: LocalStorageService, private alert: AlertService) {
    this.token.set(localStorage.getItem('token'))
    this.email.get(this.token()).subscribe((data) => {
      this.serviceId.set(data.serviceId)
      this.templateId.set(data.templateId)
      this.publicKey.set(data.publicKey)
      this.isSettingSet.set(
        !!data.publicKey &&
        !!data.serviceId &&
        !!data.templateId
      );
    })
  }

  editMode = signal<boolean>(false)

  toggleEditMode() {
    this.editMode.set(!this.editMode())
  }

  clear() {
    this.serviceId.set(null)
    this.templateId.set(null)
    this.publicKey.set(null)
  }

  submitInfo() {
    this.alert.confirm("Are You Sure?").then((conf) => {
      if (!conf.isConfirmed) return

      this.email.update(this.token(), {
        serviceId: this.serviceId(),
        templateId: this.templateId(),
        publicKey: this.publicKey()
      }).subscribe({
        next: () => this.alert.success("EmailJS Info Updated", '').then(() => {
          this.router.navigate(['/admin-panel/email-js']).then(() => window.location.reload());
        }),
        error: err => this.alert.error("EmailJS Info Not Updated", err.error)
      })
    })
  }
}
