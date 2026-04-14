import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { LocalStorageService } from '../services/local-storage-service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const localStorage = inject(LocalStorageService)

  return next(req).pipe(
    catchError((error) => {
      console.log(error.status)
      if (error.status == 401 || error.status == 403) {
        localStorage.removeItem('token')
        router.navigate(['/home'])
      }

      return throwError(() => error);
    })
  );

  return next(req);
};