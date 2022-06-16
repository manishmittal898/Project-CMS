import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupTypeAddEditComponent } from './lookup-type-add-edit.component';

describe('LookupTypeAddEditComponent', () => {
  let component: LookupTypeAddEditComponent;
  let fixture: ComponentFixture<LookupTypeAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LookupTypeAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupTypeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
