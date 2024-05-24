import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyAgreementsComponent } from './my-agreements.component';

describe('MyAgreementsComponent', () => {
  let component: MyAgreementsComponent;
  let fixture: ComponentFixture<MyAgreementsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyAgreementsComponent]
    });
    fixture = TestBed.createComponent(MyAgreementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
