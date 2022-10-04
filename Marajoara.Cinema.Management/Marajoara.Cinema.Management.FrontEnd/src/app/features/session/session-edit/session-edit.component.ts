import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Session } from 'src/app/models/Session';
import { ToastrService } from 'src/app/services/toastr.service';
import { firstValueFrom } from 'rxjs';
import { SessionService } from 'src/app/services/SessionService';

@Component({
  selector: 'app-session-edit',
  templateUrl: './session-edit.component.html',
  styleUrls: ['./session-edit.component.scss']
})
export class SessionEditComponent implements OnInit {
  sessionData!: Session;
  constructor(private router: Router,
    private route: ActivatedRoute,
    private sessionService: SessionService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadSession(id);
  }

  async loadSession(sessionId: number) {
    this.sessionData = await firstValueFrom(this.sessionService.getById(sessionId));
  }

  async onSubmit(session: Session) {
    try {
      await firstValueFrom(this.sessionService.update(session));
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }

  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigateByUrl('/sessions');
  }

}
