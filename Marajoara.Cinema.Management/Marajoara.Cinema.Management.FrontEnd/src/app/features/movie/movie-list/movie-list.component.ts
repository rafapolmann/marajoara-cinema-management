import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss']
})
export class MovieListComponent implements OnInit {

  movies!: Movie[];
  constructor(private movieService: MovieService, private router: Router) {}

  ngOnInit(): void {
    this.getAllMovies();
  }

  async getAllMovies() {
    this.movies = await firstValueFrom(this.movieService.getAll());
  }

  async onDeleteClick(movie: Movie) {
    var success = await firstValueFrom(this.movieService.delete(movie.movieID));
    if (success) {
      const index = this.movies.indexOf(movie, 0);
      if (index > -1) {
        this.movies.splice(index, 1);
      }
    }
  }

}
