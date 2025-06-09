import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageOrdersComponent} from './manage-order.component';

describe('ManageOrderComponent', () => {
  let component: ManageOrdersComponent;
  let fixture: ComponentFixture<ManageOrdersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageOrdersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
