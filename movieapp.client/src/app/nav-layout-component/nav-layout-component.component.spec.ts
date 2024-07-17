import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavLayoutComponentComponent } from './nav-layout-component.component';

describe('NavLayoutComponentComponent', () => {
  let component: NavLayoutComponentComponent;
  let fixture: ComponentFixture<NavLayoutComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavLayoutComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(NavLayoutComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
