import { Injectable } from '@angular/core';
import { CineRoom } from '../models/CineRoom';
import { Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class CineRoomService {
  private controllerUri: string = 'CineRoom';  
  constructor(private marajoaraApiService: MarajoaraApiService) {}

  getAll(): Observable<CineRoom[]> {
    return this.marajoaraApiService.get<CineRoom[]>(this.controllerUri);
  }

  getById(movieId: number): Observable<CineRoom> {
    return this.marajoaraApiService.get<CineRoom>(
      `${this.controllerUri}/${movieId}`
    );
  }

  add(cineRoom: CineRoom): Observable<number> {
    return this.marajoaraApiService.post(this.controllerUri, cineRoom);
  }

  update(cineRoom: CineRoom): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, cineRoom);
  }
  delete(cineRoomID: number):Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${cineRoomID}`)
  }
}

