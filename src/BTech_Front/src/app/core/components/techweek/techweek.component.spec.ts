import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TechweekComponent } from './techweek.component';

describe('TechweekComponent', () => {
  let component: TechweekComponent;
  let fixture: ComponentFixture<TechweekComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TechweekComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TechweekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
