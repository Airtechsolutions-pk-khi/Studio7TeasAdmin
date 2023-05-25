import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderbytypeComponent } from './orderbytype.component';

describe('OrderbytypeComponent', () => {
  let component: OrderbytypeComponent;
  let fixture: ComponentFixture<OrderbytypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderbytypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderbytypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
