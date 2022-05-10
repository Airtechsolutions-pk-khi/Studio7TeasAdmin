import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ItemsettingsComponent } from './itemsettings.component';

describe('ItemsettingsComponent', () => {
  let component: ItemsettingsComponent;
  let fixture: ComponentFixture<ItemsettingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ItemsettingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ItemsettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
