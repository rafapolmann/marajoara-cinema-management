import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {  firstValueFrom } from 'rxjs';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';

@Component({
  selector: 'app-movie-add',
  templateUrl: './movie-add.component.html',
  styleUrls: ['./movie-add.component.scss']
})
export class MovieAddComponent implements OnInit {
  constructor(private movieService: MovieService, private router: Router) {}

  ngOnInit(): void {}
   createHandler(movie: Movie) {
    this.saveMovie(movie);
    
  }  
  
  async saveMovie(movie:Movie){
    try {
      movie.movieID =  await firstValueFrom(this.movieService.add(movie));; 
      this.savePoster(movie);      
    } catch (exception:any) {
      console.log(exception);
      alert(`error status ${exception.status}; Message: ${ Object.values(exception.error)[0]}`);
    }
    
    
    
  }

  async savePoster(movie:Movie){
    if(movie.posterFile){
      await firstValueFrom(this.movieService.updatePoster(movie)); 
    }
    this.router.navigate(['/']);
  }

}
