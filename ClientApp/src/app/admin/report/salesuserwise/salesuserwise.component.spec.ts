import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesuserwiseComponent } from './salesuserwise.component';

describe('SalesuserwiseComponent', () => {
  let component: SalesuserwiseComponent;
  let fixture: ComponentFixture<SalesuserwiseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesuserwiseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesuserwiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
