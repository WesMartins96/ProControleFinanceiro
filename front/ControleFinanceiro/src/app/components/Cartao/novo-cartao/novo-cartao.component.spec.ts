import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NovoCartaoComponent } from './novo-cartao.component';

describe('NovoCartaoComponent', () => {
  let component: NovoCartaoComponent;
  let fixture: ComponentFixture<NovoCartaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NovoCartaoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NovoCartaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
