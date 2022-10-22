import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAboneComponent } from './add-abone.component';

describe('AddAboneComponent', () => {
  let component: AddAboneComponent;
  let fixture: ComponentFixture<AddAboneComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddAboneComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAboneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
