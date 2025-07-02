// auth.interceptor.ts
import { HttpRequest, HttpEvent, HttpInterceptorFn } from '@angular/common/http';
import { Observable } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: (req: HttpRequest<any>) => Observable<HttpEvent<any>>
) => {
  const token = localStorage.getItem('accessToken');
  if (token) {
    const cloned = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(cloned);
  }
  return next(req);
};
