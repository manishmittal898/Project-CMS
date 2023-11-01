import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SocialLoginCallbackComponent } from './social-login-callback.component';

describe('SocialLoginCallbackComponent', () => {
  let component: SocialLoginCallbackComponent;
  let fixture: ComponentFixture<SocialLoginCallbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SocialLoginCallbackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SocialLoginCallbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
