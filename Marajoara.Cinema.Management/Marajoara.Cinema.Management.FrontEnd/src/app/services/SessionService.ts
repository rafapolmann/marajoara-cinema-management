import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Session, SessionCommand, SessionSeat } from '../models/Session';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private controllerUri: string = 'session';
  constructor(private marajoaraApiService: MarajoaraApiService) { }

  add(sessionCommand: SessionCommand): Observable<number> {
    return this.marajoaraApiService.post(this.controllerUri, sessionCommand);
  }

  update(sessionCommand: SessionCommand): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, sessionCommand);
  }

  delete(sessionID: number): Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${sessionID}`)
  }

  getById(sessionId: number): Observable<Session> {
    return this.marajoaraApiService.get<Session>(
      `${this.controllerUri}/${sessionId}`
    );
  }
  getOccupiedSeats(sessionId: number): Observable<SessionSeat[]> {
    return this.marajoaraApiService.get<SessionSeat[]>(
      `${this.controllerUri}/${sessionId}/occupiedseats`
    );
  }

  getAll(): Observable<Session[]> {
    return this.marajoaraApiService.get<Session[]>(this.controllerUri);
  }
}
