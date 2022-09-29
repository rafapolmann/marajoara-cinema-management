import { CineRoom } from './CineRoom';
import { Movie } from './Movie';

export interface Session {
  sessionID: number;
  sessionDate: Date;
  endSession: Date;
  price: number;
  movie: Movie;
  cineRoom: CineRoom;
}
