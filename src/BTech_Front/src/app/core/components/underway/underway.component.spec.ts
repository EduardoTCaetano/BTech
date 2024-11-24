import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnderwayComponent } from './underway.component';

describe('UnderwayComponent', () => {
  let component: UnderwayComponent;
  let fixture: ComponentFixture<UnderwayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UnderwayComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnderwayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
