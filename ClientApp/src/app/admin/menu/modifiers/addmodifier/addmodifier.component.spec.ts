import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddmodifierComponent } from './addmodifier.component';

describe('AddmodifierComponent', () => {
  let component: AddmodifierComponent;
  let fixture: ComponentFixture<AddmodifierComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddmodifierComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddmodifierComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
