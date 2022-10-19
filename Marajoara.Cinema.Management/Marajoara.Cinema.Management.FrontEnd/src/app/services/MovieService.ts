import { Injectable } from '@angular/core';
import { Movie, MovieFull } from '../models/Movie';
import { Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';


@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private controllerUri: string = 'movie';
  private inTheaterUri:string = 'InTheater' 
  private posterUri: string = `poster`;
   
  constructor(private marajoaraApiService: MarajoaraApiService) {}

  getAll(): Observable<Movie[]> {
    return this.marajoaraApiService.get<Movie[]>(this.controllerUri);
  }

  getById(movieId: number): Observable<Movie> {
    return this.marajoaraApiService.get<Movie>(
      `${this.controllerUri}/${movieId}`
    );
  }

  getInTheater(): Observable<MovieFull[]> {
    const baseDate = new Date();
    const startDate:string = baseDate.toISOString();
    baseDate.setDate(baseDate.getDate()+30) ;
    const endDate:string = baseDate.toISOString();    
    return this.marajoaraApiService.get<MovieFull[]>(`${this.controllerUri}/${this.inTheaterUri}/${startDate}/${endDate}`);
  }

  getInTheaterMovieDetails(movieID:number): Observable<MovieFull> {
    const baseDate = new Date();
    const startDate:string = baseDate.toISOString();
    baseDate.setDate(baseDate.getDate()+30) ;
    const endDate:string = baseDate.toISOString();    
    return this.marajoaraApiService.get<MovieFull>(`${this.controllerUri}/${this.inTheaterUri}/${movieID}/${startDate}/${endDate}`);
  }

  getPosterById(movieId: number): Observable<string> {
    return this.marajoaraApiService.get<string>(
      `${this.controllerUri}/${movieId}/${this.posterUri}`
    );
  }

  add(movie: Movie): Observable<number> {
    return this.marajoaraApiService.post(this.controllerUri, movie);
  }

  update(movie: Movie): Observable<boolean> {
    return this.marajoaraApiService.put(this.controllerUri, movie);
  }

  updatePoster(movie: Movie):Observable<boolean> {
    const formData = new FormData();
    formData.append('file', movie.posterFile);

    return this.marajoaraApiService.put(
      `${this.controllerUri}/${movie.movieID}/${this.posterUri}`,
      formData
    );
  }

  delete(movieId: number):Observable<boolean> {
    return this.marajoaraApiService.delete(`${this.controllerUri}/${movieId}`)
  }
}

