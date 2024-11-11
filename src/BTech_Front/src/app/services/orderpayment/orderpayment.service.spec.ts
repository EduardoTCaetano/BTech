import { TestBed } from '@angular/core/testing';

import { OrderPaymentService } from './orderpayment.service';

describe('OrderPaymentService', () => {
  let service: OrderPaymentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrderPaymentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
