import { Injectable } from '@angular/core';
import emailjs, { type EmailJSResponseStatus } from '@emailjs/browser'

@Injectable({
  providedIn: 'root',
})

export class Email {

  serviceId: string = "service_kqw395h"
  templateId: string = "template_75iei9r"
  publicKey: string = "90LyXpeSeVnNPQeFJ"

  sendContact(firstName: string, lastName: string, email: string, message: string) {
    return emailjs.send(this.serviceId, this.templateId, {
      firstName: firstName,
      lastName: lastName,
      email: email,
      message: message
    }, { publicKey: this.publicKey }
    )
  }
}
