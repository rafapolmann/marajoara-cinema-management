import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Session } from '../models/Session';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private controllerUri: string = 'session';
  constructor(private marajoaraApiService: MarajoaraApiService) {}

  delete(sessionID: number):Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${sessionID}`)
  }

  getAll(): Observable<Session[]> {
    return this.marajoaraApiService.get<Session[]>(this.controllerUri);
  }
}
