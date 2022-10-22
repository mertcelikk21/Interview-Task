import { TestBed } from '@angular/core/testing';

import { AboneService } from './abone.service';

describe('AboneService', () => {
  let service: AboneService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AboneService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
