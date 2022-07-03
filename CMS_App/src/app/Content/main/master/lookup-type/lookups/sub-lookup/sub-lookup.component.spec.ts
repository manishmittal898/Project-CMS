import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubLookupComponent } from './sub-lookup.component';

describe('SubLookupComponent', () => {
  let component: SubLookupComponent;
  let fixture: ComponentFixture<SubLookupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubLookupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubLookupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
