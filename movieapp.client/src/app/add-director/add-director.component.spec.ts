import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDirectorComponent } from './add-director.component';

describe('AddDirectorComponent', () => {
  let component: AddDirectorComponent;
  let fixture: ComponentFixture<AddDirectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddDirectorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddDirectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
