import { Injectable } from '@angular/core';
import { Movie } from '../models/Movie';
import { Observable } from 'rxjs';
import { MarajoaraApiService } from './MarajoaraApiService';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private controllerUri: string = 'movie';
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
