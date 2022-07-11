import { TestBed } from '@angular/core/testing';
import { CDocCookieService } from "./cdoc.cookie.service"

describe('CDocCookieService', () => {
  let service: CDocCookieService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CDocCookieService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
