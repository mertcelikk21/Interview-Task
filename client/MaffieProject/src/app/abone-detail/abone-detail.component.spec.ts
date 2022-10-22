import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboneDetailComponent } from './abone-detail.component';

describe('AboneDetailComponent', () => {
  let component: AboneDetailComponent;
  let fixture: ComponentFixture<AboneDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AboneDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AboneDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
