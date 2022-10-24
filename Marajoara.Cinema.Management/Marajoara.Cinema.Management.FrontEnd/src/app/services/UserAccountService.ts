import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';
import { UserAccount, UserAccountChangePassword } from '../models/UserAccount';

@Injectable({
  providedIn: 'root',
})
export class UserAccountService {
  private controllerUri: string = 'UserAccount';
  private photoUri: string = `photo`;

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
    return this.marajoaraApiService.post(this.getAddUrl(userAccount.level), userAccount);
  }

  private getAddUrl(accessLevel: number): string {
    switch (accessLevel) {
      case 1:
        return `${this.controllerUri}/manager`;
      case 2:
        return `${this.controllerUri}/attendant`;
      case 3:
        return `${this.controllerUri}/customer`;
      default:
        return "";
    }
  }

  update(userAccount: UserAccount): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, userAccount);
  }

  delete(userAccountId: number): Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${userAccountId}`)
  }

  getPhotoByUserId(userAccountId: number): Observable<string> {
    return this.marajoaraApiService.get<string>(`${this.controllerUri}/${userAccountId}/${this.photoUri}`);
  }

  updatePhoto(userAccount: UserAccount): Observable<boolean> {
    const formData = new FormData();
    formData.append('file', userAccount.photoFile!);

    return this.marajoaraApiService.put(
      `${this.controllerUri}/${userAccount.userAccountID}/${this.photoUri}`,
      formData
    );
  }

  deletePhoto(userAccountId: number): Observable<string> {
    return this.marajoaraApiService.delete<string>(`${this.controllerUri}/${userAccountId}/${this.photoUri}`);
  }

  resetPassword(userAccount: UserAccount): Observable<boolean> {
    return this.marajoaraApiService.post(`${this.controllerUri}/reset-password`, userAccount);
  }

  changePassword(userAccountChangePassword: UserAccountChangePassword): Observable<boolean> {
    return this.marajoaraApiService.post(`${this.controllerUri}/change-password`, userAccountChangePassword);
  }
}
