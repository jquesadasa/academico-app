import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { NotificationService } from '../services/notification.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notificationService = inject(NotificationService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      const message =
        typeof error.error === 'string'
          ? error.error
          : 'Ocurrio un error al comunicarse con el servidor.';

      notificationService.error(message);
      return throwError(() => error);
    })
  );
};
