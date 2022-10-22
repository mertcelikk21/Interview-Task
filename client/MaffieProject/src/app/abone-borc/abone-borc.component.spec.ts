import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboneBorcComponent } from './abone-borc.component';

describe('AboneBorcComponent', () => {
  let component: AboneBorcComponent;
  let fixture: ComponentFixture<AboneBorcComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AboneBorcComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AboneBorcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
