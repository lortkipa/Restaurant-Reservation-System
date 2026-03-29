import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  
  success(text: string) {
    Swal.fire({
      title: 'Success!',
      text: text,
      icon: 'success',
      confirmButtonText: 'OK'
    });
  }

  error(text: string) {
    Swal.fire({
      title: 'Error!',
      text: text,
      icon: "error",
      confirmButtonText: 'cool'
    })
  }

}
