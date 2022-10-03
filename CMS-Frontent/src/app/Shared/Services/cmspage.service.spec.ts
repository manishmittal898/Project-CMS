import { TestBed } from '@angular/core/testing';

import { CMSPageService } from './cmspage.service';

describe('CMSPageService', () => {
  let service: CMSPageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CMSPageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
