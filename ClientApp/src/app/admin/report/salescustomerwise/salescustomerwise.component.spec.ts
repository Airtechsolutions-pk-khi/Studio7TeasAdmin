import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalescustomerwiseComponent } from './salescustomerwise.component';

describe('SalescustomerwiseComponent', () => {
  let component: SalescustomerwiseComponent;
  let fixture: ComponentFixture<SalescustomerwiseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalescustomerwiseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalescustomerwiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
