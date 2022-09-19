import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieAddComponent } from './movie-add.component';

describe('MovieAddComponent', () => {
  let component: MovieAddComponent;
  let fixture: ComponentFixture<MovieAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MovieAddComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MovieAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
