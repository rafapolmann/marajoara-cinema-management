import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CineroomAddComponent } from './features/cineroom/cineroom-add/cineroom-add.component';
import { CineroomEditComponent } from './features/cineroom/cineroom-edit/cineroom-edit.component';
import { CineroomListComponent } from './features/cineroom/cineroom-list/cineroom-list.component';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';


const routes: Routes = [
  { path: '', component: MovieListComponent},    
  { path: 'movies', component: MovieListComponent},    
  { path: 'movie/:id/edit', component: MovieEditComponent },
  { path: 'newmovie', component: MovieAddComponent },

  { path: 'cineroom', component: CineroomListComponent},    
  { path: 'newcineroom', component: CineroomAddComponent },
  { path: 'cineroom/:id/edit', component: CineroomEditComponent },

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation:'reload'})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
