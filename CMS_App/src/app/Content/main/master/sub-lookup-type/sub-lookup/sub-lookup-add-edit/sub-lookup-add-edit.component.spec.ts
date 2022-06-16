import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubLookupAddEditComponent } from './sub-lookup-add-edit.component';

describe('SubLookupAddEditComponent', () => {
  let component: SubLookupAddEditComponent;
  let fixture: ComponentFixture<SubLookupAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubLookupAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SubLookupAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
