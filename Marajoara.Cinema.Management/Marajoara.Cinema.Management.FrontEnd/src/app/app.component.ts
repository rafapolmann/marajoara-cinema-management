import { ChangeDetectorRef, AfterContentChecked, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { AccessLevel, AuthorizedUserAccount } from './models/UserAccount';
import { AuthenticationService } from './services/authentication.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements AfterContentChecked {
  title = 'Cine Marajoara';
  user!: AuthorizedUserAccount;

  @ViewChild('sidenav') sidenav!: MatSidenav;
  constructor(
    public loadingService: LoadingService,
    private changeDetector: ChangeDetectorRef,
    private authService: AuthenticationService,
  ) {
    this.authService.user.subscribe(u => this.userChanged(u));
  }

  private userChanged(u: AuthorizedUserAccount) {
    this.user = u;
  }

  public get AccessLevel() {
    return AccessLevel;
  }

  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }

  toggleSideNav() {
    if (this.sidenav) {
      this.sidenav.toggle()
    }
  }

  logout() {
    this.authService.logout();
  }
}
