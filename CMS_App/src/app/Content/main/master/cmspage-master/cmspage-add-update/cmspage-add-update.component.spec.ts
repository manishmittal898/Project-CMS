import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CMSPageAddUpdateComponent } from './cmspage-add-update.component';

describe('CMSPageAddUpdateComponent', () => {
  let component: CMSPageAddUpdateComponent;
  let fixture: ComponentFixture<CMSPageAddUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CMSPageAddUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CMSPageAddUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
