import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { PugComponent } from './pug.component';

describe('PugComponent', () => {
  let component: PugComponent;
  let fixture: ComponentFixture<PugComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PugComponent ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(PugComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
