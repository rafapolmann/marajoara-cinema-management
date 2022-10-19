import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
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
import { LoginComponent } from './features/user/login/login/login.component';
import { RegisterComponent } from './features/user/register/register/register.component';
import { UserAccountAddComponent } from './features/user/user-account-add/user-account-add.component';
import { UserAccountEditComponent } from './features/user/user-account-edit/user-account-edit.component';
import { UserAccountListComponent } from './features/user/user-account-list/user-account-list.component';

const routes: Routes = [
  { path: '', component: MovieInTheaterComponent},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'in-theater', component: MovieInTheaterComponent , canActivate:[AuthGuard] },  
  { path: 'in-theater/:id/details', component: MovieDetailsComponent , canActivate:[AuthGuard] },
  { path: 'movies', component: MovieListComponent , canActivate:[AuthGuard] },
  { path: 'movie/:id/edit', component: MovieEditComponent , canActivate:[AuthGuard] },
  { path: 'newmovie', component: MovieAddComponent , canActivate:[AuthGuard] },
  { path: 'cinerooms', component: CineroomListComponent , canActivate:[AuthGuard] },
  { path: 'newcineroom', component: CineroomAddComponent , canActivate:[AuthGuard] },
  { path: 'cineroom/:id/edit', component: CineroomEditComponent , canActivate:[AuthGuard] },
  { path: 'sessions', component: SessionListComponent , canActivate:[AuthGuard] },
  { path: 'newsession', component: SessionAddComponent , canActivate:[AuthGuard] },
  { path: 'session/:id/edit', component: SessionEditComponent , canActivate:[AuthGuard] },
  { path: 'users', component: UserAccountListComponent , canActivate:[AuthGuard] },
  { path: 'newuseraccount', component: UserAccountAddComponent , canActivate:[AuthGuard] },
  { path: 'useraccount/:id/edit', component: UserAccountEditComponent , canActivate:[AuthGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
