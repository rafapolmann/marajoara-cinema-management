import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Movie } from 'src/app/Models/Movie';
import { firstValueFrom } from 'rxjs';
import { MovieService } from 'src/app/services/MovieService';
import { ToastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-movie-edit',
  templateUrl: './movie-edit.component.html',
  styleUrls: ['./movie-edit.component.scss']
})
export class MovieEditComponent implements OnInit {
  movieData!: Movie;
  movieAux!: Movie;
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private movieService: MovieService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadMovie(id);
  }

  async loadMovie(movieId: number) {
    this.movieAux = await firstValueFrom(this.movieService.getById(movieId));
    this.loadPoster(this.movieAux.movieID);
  }

  async loadPoster(movieId: number) {
    this.movieAux.poster = await firstValueFrom(this.movieService.getPosterById(movieId));
    this.movieData = this.movieAux;
  }

  async onSubmit(movie: Movie) {
    try {
      await firstValueFrom(this.movieService.update(movie));
      if (movie.posterFile) {
        await firstValueFrom(this.movieService.updatePoster(movie));
      }
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }
  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigateByUrl('/movies');
  }
}
