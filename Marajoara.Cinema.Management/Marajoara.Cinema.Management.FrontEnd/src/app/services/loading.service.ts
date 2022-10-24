import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

/**
 * Service used to centralize the loading status throughout the application
 */
@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  loading$:Observable<boolean> = this.loadingSubject.asObservable();

  show():void {
    this.loadingSubject.next(true);
  }

  hide():void {
    this.loadingSubject.next(false);
  }
}