import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), HttpClientModule, FormBuilder],
};
