import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmAltaComponent } from './em-alta.component';

describe('EmAltaComponent', () => {
  let component: EmAltaComponent;
  let fixture: ComponentFixture<EmAltaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmAltaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmAltaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
