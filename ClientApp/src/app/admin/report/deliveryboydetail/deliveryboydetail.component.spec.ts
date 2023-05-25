import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryboydetailComponent } from './deliveryboydetail.component';

describe('DeliveryboydetailComponent', () => {
  let component: DeliveryboydetailComponent;
  let fixture: ComponentFixture<DeliveryboydetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryboydetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryboydetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
