import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';
import { AuthorizedUserAccount } from '../models/UserAccount';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private controllerUri: string = 'Authorization';

  private userSubject!: BehaviorSubject<AuthorizedUserAccount>;
  public user!: Observable<AuthorizedUserAccount>;

  constructor(private marajoaraApiService: MarajoaraApiService,
    private router: Router,) {

    this.userSubject = new BehaviorSubject<AuthorizedUserAccount>(this.getUserFromStorage());
    this.user = this.userSubject.asObservable();
  }

  login(email: string, password: string): Observable<AuthorizedUserAccount> {
    return this.marajoaraApiService.post<AuthorizedUserAccount>(`${this.controllerUri}/login`, { mail: email, password: password })
      .pipe(map(u => {
        this.saveUserLocalStorage(u);
        this.userSubject.next(u);
        return u;
      }));
  }

  register(name: string, email: string) {
    return this.marajoaraApiService.post<number>(`${this.controllerUri}/register`, { name: name, mail: email });
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(this.getUserFromStorage());
    this.router.navigateByUrl('/login');
  }

  public get authorizedUserAccount(): AuthorizedUserAccount {
    return this.userSubject.value;
  }

  private getUserFromStorage(): any {
    const userJson = localStorage.getItem('user');
    if (!userJson)
      return undefined;

    return JSON.parse(userJson!) as AuthorizedUserAccount;
  }

  private saveUserLocalStorage(user: AuthorizedUserAccount) {
    localStorage.setItem('user', JSON.stringify(user));
  }
}
