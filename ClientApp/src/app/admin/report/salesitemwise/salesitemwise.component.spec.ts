import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalesitemwiseComponent } from './salesitemwise.component';

describe('SalesitemwiseComponent', () => {
  let component: SalesitemwiseComponent;
  let fixture: ComponentFixture<SalesitemwiseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalesitemwiseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalesitemwiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
