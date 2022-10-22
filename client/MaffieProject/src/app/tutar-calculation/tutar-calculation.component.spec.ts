import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TutarCalculationComponent } from './tutar-calculation.component';

describe('TutarCalculationComponent', () => {
  let component: TutarCalculationComponent;
  let fixture: ComponentFixture<TutarCalculationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TutarCalculationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TutarCalculationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
