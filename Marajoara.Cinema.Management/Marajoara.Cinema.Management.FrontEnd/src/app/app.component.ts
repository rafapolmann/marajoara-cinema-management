import { ChangeDetectorRef, AfterContentChecked, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements AfterContentChecked {
  title = 'Cine Marajoara';
  user: unknown;
  @ViewChild('sidenav') sidenav!: MatSidenav;
  constructor(
    public loadingService: LoadingService,
    private changeDetector: ChangeDetectorRef
  ) {
    this.user = undefined;
  }

  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }
  toggleSideNav(){
    if(this.sidenav){
      this.sidenav.toggle()
    }
  }
}
