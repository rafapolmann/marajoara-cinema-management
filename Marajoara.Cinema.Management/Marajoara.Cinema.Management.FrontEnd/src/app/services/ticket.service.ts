import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketCommand, TicketFlat } from '../models/Ticket';
import { AuthenticationService } from './authentication.service';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  private controllerUri: string = 'ticket';
  constructor(private marajoaraApiService: MarajoaraApiService,
    private authservice: AuthenticationService,
  ) { }


  add(sessionID: number, seatNumber: number): Observable<number> {
    const ticket: TicketCommand =
    {
      sessionID: sessionID,
      seatNumber: seatNumber,
      userAccountID: this.authservice.authorizedUserAccount.userAccountID,
    };
    return this.marajoaraApiService.post<number>(this.controllerUri, ticket);

  }
  
  setAsUsed(ticketCode:string):Observable<boolean>{
    return this.marajoaraApiService.post<boolean>(`${this.controllerUri}/used`,{ticketCode:ticketCode});
  }

  delete(ticketID: number): Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${ticketID}`)
  }

  getById(ticketID: number): Observable<TicketFlat> {
    return this.marajoaraApiService.get<TicketFlat>(
      `${this.controllerUri}/${ticketID}`
    );
  }

  getAll(): Observable<TicketFlat[]> {
    return this.marajoaraApiService.get<TicketFlat[]>(this.controllerUri);
  }

  getByCurrentUser() {
    const userAccountID:number  = this.authservice.authorizedUserAccount.userAccountID;
    return this.marajoaraApiService.get<TicketFlat[]>(`${this.controllerUri}/user/${userAccountID}`);
  }


}
