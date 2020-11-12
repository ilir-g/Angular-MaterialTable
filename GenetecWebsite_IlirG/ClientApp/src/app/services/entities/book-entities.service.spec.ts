import { TestBed } from '@angular/core/testing';

import { BookEntitiesService } from './book-entities.service';

describe('BookEntitiesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BookEntitiesService = TestBed.get(BookEntitiesService);
    expect(service).toBeTruthy();
  });
});
