import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TreeLabelComponent } from './tree-label.component';

describe('TreeLabelComponent', () => {
  let component: TreeLabelComponent;
  let fixture: ComponentFixture<TreeLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TreeLabelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TreeLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
