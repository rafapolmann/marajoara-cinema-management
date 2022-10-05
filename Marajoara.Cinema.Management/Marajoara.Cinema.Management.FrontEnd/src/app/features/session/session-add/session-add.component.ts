import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Session, SessionCommand } from 'src/app/models/Session';
import { SessionService } from 'src/app/services/SessionService';
import { ToastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-session-add',
  templateUrl: './session-add.component.html',
  styleUrls: ['./session-add.component.scss'],
})
export class SessionAddComponent implements OnInit {
  constructor(
    private sessionService: SessionService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void { }

  onSubmit(sessionCommand: SessionCommand) {
    this.saveSession(sessionCommand);
  }

  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigate(['/sessions']);
  }

  async saveSession(sessionCommand: SessionCommand) {
    try {
      sessionCommand.sessionID = await firstValueFrom(
        this.sessionService.add(sessionCommand)
      );
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status} - ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }
}
