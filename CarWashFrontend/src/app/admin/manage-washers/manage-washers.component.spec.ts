import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageWashersComponent } from './manage-washers.component';

describe('ManageWashersComponent', () => {
  let component: ManageWashersComponent;
  let fixture: ComponentFixture<ManageWashersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageWashersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageWashersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
