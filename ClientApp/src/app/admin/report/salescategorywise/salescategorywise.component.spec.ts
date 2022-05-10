import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalescategorywiseComponent } from './salescategorywise.component';

describe('SalescategorywiseComponent', () => {
  let component: SalescategorywiseComponent;
  let fixture: ComponentFixture<SalescategorywiseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalescategorywiseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalescategorywiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
