import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { TotastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-movie-add',
  templateUrl: './movie-add.component.html',
  styleUrls: ['./movie-add.component.scss'],
})
export class MovieAddComponent implements OnInit {
  constructor(
    private movieService: MovieService,
    private router: Router,
    private toastr: TotastrService
  ) {}

  ngOnInit(): void {}

  onSubmit(movie: Movie) {
    this.saveMovie(movie);
  }

  onCancel() {
    this.navigateToList();
  }

  async saveMovie(movie: Movie) {
    try {
      movie.movieID = await firstValueFrom(this.movieService.add(movie));
      await this.savePoster(movie);
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }

  async savePoster(movie: Movie) {
    if (movie.posterFile) {
      await firstValueFrom(this.movieService.updatePoster(movie));
    }
  }

  navigateToList() {
    this.router.navigateByUrl('/movies');
  }
}
