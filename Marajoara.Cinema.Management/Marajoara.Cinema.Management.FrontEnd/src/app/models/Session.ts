import { CineRoom } from './CineRoom';
import { Movie } from './Movie';

export interface Session {
  sessionID: number;
  sessionDate: Date;
  endSession?: Date;
  price: number;
  movie: Movie;
  cineRoom: CineRoom;
}

export interface SessionCommand {
  sessionID: number;
  sessionDate: string;
  price: number;
  movieID: number;
  cineRoomID: number;
}

export interface SessionFlat{
  sessionID :number,
   sessionDate :Date,
    endSession:Date,
     price:number,
     cineRoomID:number,
}

export interface SessionSeat {
  sessionID:number,
  seatNumber:number,
}