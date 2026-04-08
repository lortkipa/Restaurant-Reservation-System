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

  getPicture(text: string) {
    return Swal.fire({
      title: text,
      input: 'file',

      inputAttributes: {
        accept: '.png,.jpg,.jpeg,.webp',
        'aria-label': 'Upload your profile picture'
      },

      confirmButtonText: 'Upload',

      showDenyButton: true,
      denyButtonText: 'Remove Picture',

      showCancelButton: true,
      cancelButtonText: 'Cancel'
    });
  }
}
