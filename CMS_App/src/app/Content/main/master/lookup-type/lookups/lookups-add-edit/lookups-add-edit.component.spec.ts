import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LookupsAddEditComponent } from './lookups-add-edit.component';

describe('LookupsAddEditComponent', () => {
  let component: LookupsAddEditComponent;
  let fixture: ComponentFixture<LookupsAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LookupsAddEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LookupsAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
