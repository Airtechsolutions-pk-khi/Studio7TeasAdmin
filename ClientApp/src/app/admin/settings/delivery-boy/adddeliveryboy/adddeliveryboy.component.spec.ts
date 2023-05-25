import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdddeliveryboyComponent } from './adddeliveryboy.component';

describe('AdddeliveryboyComponent', () => {
  let component: AdddeliveryboyComponent;
  let fixture: ComponentFixture<AdddeliveryboyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdddeliveryboyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdddeliveryboyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
