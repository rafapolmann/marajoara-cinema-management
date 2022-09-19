import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';

@Component({
  selector: 'app-movie-card',
  templateUrl: './movie-card.component.html',
  styleUrls: ['./movie-card.component.scss']
})
export class MovieCardComponent implements OnInit {
  @Input() movie!:Movie;
  moviePoster:string= '';
  constructor(private movieService: MovieService, private route: ActivatedRoute) {
    //  const id = Number( this.route.snapshot.paramMap.get("id"));
    //  this.loadMovie(id);          
    }

  ngOnInit(): void {
    //Lazyloads the poster
    this.loadPoster(this.movie.movieID);
  }
  
  async loadPoster(movieId:number){    
    this.moviePoster = await firstValueFrom(this.movieService.getPosterById(movieId));
  }

}
