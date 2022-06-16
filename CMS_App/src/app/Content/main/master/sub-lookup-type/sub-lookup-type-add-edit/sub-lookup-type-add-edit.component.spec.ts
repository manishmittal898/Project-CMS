import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubLookupTypeAddEditComponent } from './sub-lookup-type-add-edit.component';

describe('SubLookupTypeAddEditComponent', () => {
  let component: SubLookupTypeAddEditComponent;
  let fixture: ComponentFixture<SubLookupTypeAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubLookupTypeAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubLookupTypeAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
