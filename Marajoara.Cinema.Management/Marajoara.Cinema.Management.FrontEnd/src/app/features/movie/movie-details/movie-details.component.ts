import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { MovieFull } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss'],
})
export class MovieDetailsComponent implements OnInit {
  movie!:MovieFull;
  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) {}
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadMovie(id);
  }

  async loadMovie(movieId: number) {
    this.movie = await firstValueFrom(this.movieService.getInTheaterMovieDetails(movieId));    
  }
}
