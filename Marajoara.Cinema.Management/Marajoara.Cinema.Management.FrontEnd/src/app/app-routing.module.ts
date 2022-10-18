import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CineroomAddComponent } from './features/cineroom/cineroom-add/cineroom-add.component';
import { CineroomEditComponent } from './features/cineroom/cineroom-edit/cineroom-edit.component';
import { CineroomListComponent } from './features/cineroom/cineroom-list/cineroom-list.component';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieDetailsComponent } from './features/movie/movie-details/movie-details.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieInTheaterComponent } from './features/movie/movie-in-theater/movie-in-theater.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';
import { SessionAddComponent } from './features/session/session-add/session-add.component';
import { SessionEditComponent } from './features/session/session-edit/session-edit.component';
import { SessionListComponent } from './features/session/session-list/session-list.component';
import { UserAccountAddComponent } from './features/user/user-account-add/user-account-add.component';
import { UserAccountEditComponent } from './features/user/user-account-edit/user-account-edit.component';
import { UserAccountListComponent } from './features/user/user-account-list/user-account-list.component';

const routes: Routes = [
  { path: '', component: MovieInTheaterComponent },
  { path: 'in-theater', component: MovieInTheaterComponent },  
  { path: 'in-theater/:id/details', component: MovieDetailsComponent },
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
  { path: 'newuseraccount', component: UserAccountAddComponent },
  { path: 'useraccount/:id/edit', component: UserAccountEditComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
