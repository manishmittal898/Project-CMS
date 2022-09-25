import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CMSPageMasterComponent } from './cmspage-master.component';

describe('CMSPageMasterComponent', () => {
  let component: CMSPageMasterComponent;
  let fixture: ComponentFixture<CMSPageMasterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CMSPageMasterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CMSPageMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
