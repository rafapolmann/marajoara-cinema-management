import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';
import { SessionListComponent } from './features/session/session-list/session-list.component';

const routes: Routes = [
  { path: '', component: MovieListComponent },
  { path: 'movies', component: MovieListComponent },
  { path: 'movie/:id/edit', component: MovieEditComponent },
  { path: 'newmovie', component: MovieAddComponent },
  { path: 'sessions', component: SessionListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
