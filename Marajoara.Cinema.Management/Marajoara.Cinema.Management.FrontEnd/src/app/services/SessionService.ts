import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Session } from '../models/Session';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private controllerUri: string = 'session';
  constructor(private marajoaraApiService: MarajoaraApiService) { }

  add(session: Session): Observable<number> {
    return this.marajoaraApiService.post(this.controllerUri, session);
  }

  update(session: Session): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, session);
  }

  delete(sessionID: number): Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${sessionID}`)
  }

  getById(sessionId: number): Observable<Session> {
    console.log(`${this.controllerUri}/${sessionId}`);
    return this.marajoaraApiService.get<Session>(
      `${this.controllerUri}/${sessionId}`
    );
  }

  getAll(): Observable<Session[]> {
    return this.marajoaraApiService.get<Session[]>(this.controllerUri);
  }
}
