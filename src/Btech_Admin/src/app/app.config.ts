import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';
import { routes } from './app.routes';
import { authInterceptor } from './services/auth/interceptor/authinterceptor.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    FormBuilder,
    provideHttpClient(
      withInterceptors([authInterceptor])
    )
  ],
};
