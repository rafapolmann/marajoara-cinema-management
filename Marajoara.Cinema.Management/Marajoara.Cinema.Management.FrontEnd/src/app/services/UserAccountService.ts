import { Injectable } from '@angular/core';
import { CineRoom } from '../models/CineRoom';
import { Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';
import { UserAccount } from '../models/UserAccount';

@Injectable({
  providedIn: 'root',
})
export class UserAccountService {
  private controllerUri: string = 'UserAccount';
  constructor(private marajoaraApiService: MarajoaraApiService) { }

  getAll(): Observable<UserAccount[]> {
    return this.marajoaraApiService.get<UserAccount[]>(this.controllerUri);
  }

  getById(userAccountId: number): Observable<UserAccount> {
    return this.marajoaraApiService.get<UserAccount>(
      `${this.controllerUri}/${userAccountId}`
    );
  }

  add(userAccount: UserAccount): Observable<number> {
    return this.marajoaraApiService.post(this.controllerUri, userAccount);
  }

  update(userAccount: UserAccount): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, userAccount);
  }

  delete(userAccountId: number): Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${userAccountId}`)
  }
}
