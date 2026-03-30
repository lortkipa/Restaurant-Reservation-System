import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  
  success(title: string, text: string) {
    return Swal.fire({
      title: title,
      text: text,
      icon: 'success',
      confirmButtonText: 'OK'
    });
  }

  error(title: string, text: string) {
    return Swal.fire({
      title: title,
      text: text,
      icon: "error",
      confirmButtonText: 'ok'
    })
  }

  confirm(question: string) {
    return Swal.fire({
      title: question,
      text: '',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'yes',
      cancelButtonText: 'no',
      reverseButtons: true
    })
  }

}
