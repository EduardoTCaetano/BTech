import { TestBed } from '@angular/core/testing';

import { MercadoPagoServiceService } from './mercado-pago-service.service';

describe('MercadoPagoServiceService', () => {
  let service: MercadoPagoServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MercadoPagoServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
