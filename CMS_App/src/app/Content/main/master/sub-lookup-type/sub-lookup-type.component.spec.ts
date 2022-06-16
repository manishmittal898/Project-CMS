import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubLookupTypeComponent } from './sub-lookup-type.component';

describe('SubLookupTypeComponent', () => {
  let component: SubLookupTypeComponent;
  let fixture: ComponentFixture<SubLookupTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubLookupTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubLookupTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
