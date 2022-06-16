import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupTypeComponent } from './lookup-type.component';

describe('LookupTypeComponent', () => {
  let component: LookupTypeComponent;
  let fixture: ComponentFixture<LookupTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LookupTypeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
