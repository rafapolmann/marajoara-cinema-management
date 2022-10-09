import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CineroomAddComponent } from './features/cineroom/cineroom-add/cineroom-add.component';
import { CineroomEditComponent } from './features/cineroom/cineroom-edit/cineroom-edit.component';
import { CineroomListComponent } from './features/cineroom/cineroom-list/cineroom-list.component';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';
import { SessionAddComponent } from './features/session/session-add/session-add.component';
import { SessionEditComponent } from './features/session/session-edit/session-edit.component';
import { SessionListComponent } from './features/session/session-list/session-list.component';
import { UserAccountListComponent } from './features/user/user-account-list/user-account-list.component';

const routes: Routes = [
  { path: '', component: MovieListComponent },
  { path: 'movies', component: MovieListComponent },
  { path: 'movie/:id/edit', component: MovieEditComponent },
  { path: 'newmovie', component: MovieAddComponent },
  { path: 'cinerooms', component: CineroomListComponent },
  { path: 'newcineroom', component: CineroomAddComponent },
  { path: 'cineroom/:id/edit', component: CineroomEditComponent },
  { path: 'sessions', component: SessionListComponent },
  { path: 'newsession', component: SessionAddComponent },
  { path: 'session/:id/edit', component: SessionEditComponent },
  { path: 'users', component: UserAccountListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
