import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Session } from 'src/app/models/Session';
import { SessionService } from 'src/app/services/SessionService';

@Component({
  selector: 'app-session-add',
  templateUrl: './session-add.component.html',
  styleUrls: ['./session-add.component.scss'],
})
export class SessionAddComponent implements OnInit {
  constructor(private sessionService: SessionService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(session: Session) {
    this.saveSession(session);
  }

  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigate(['/sessions']);
  }

  async saveSession(session: Session) {
    try {
      session.sessionID = await firstValueFrom(
        this.sessionService.add(session)
      );
    } catch (exception: any) {
      console.log(exception);
      alert(
        `error status ${exception.status}; Message: ${
          Object.values(exception.error)[0]
        }`
      );
    } finally {
      this.navigateToList();
    }
  }
}
