import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AdditemsComponent } from './additem.component';



describe('AdditemsComponent', () => {
  let component: AdditemsComponent;
  let fixture: ComponentFixture<AdditemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdditemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdditemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
