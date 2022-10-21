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
import { UserAccountProfileComponent } from './features/user/user-account-profile/user-account-profile.component';
import { AccessLevel } from './models/UserAccount'


/** USER MODULE ROUTES DEFINITION */
const userModuleRoutes: Routes = [
  {
    path: 'users', component: UserAccountListComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'newuseraccount', component: UserAccountAddComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'useraccount/:id/edit', component: UserAccountEditComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  { path: 'userprofile', component: UserAccountProfileComponent, canActivate: [AuthGuard] },
];

/** MOVIE MODULE ROUTES DEFINITION */
const movieModuleRoutes: Routes = [
  { path: '', component: MovieInTheaterComponent, canActivate: [AuthGuard] },
  { path: 'in-theater', component: MovieInTheaterComponent, canActivate: [AuthGuard] },
  { path: 'in-theater/:id/details', component: MovieDetailsComponent, canActivate: [AuthGuard] },
  {
    path: 'movies', component: MovieListComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'movie/:id/edit', component: MovieEditComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'newmovie', component: MovieAddComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
];

/** CINEROOM MODULE ROUTES DEFINITION */
const cineRoomModuleRoutes: Routes = [
  {
    path: 'cinerooms', component: CineroomListComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'newcineroom', component: CineroomAddComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'cineroom/:id/edit', component: CineroomEditComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
];
/** SESSION MODULE ROUTES DEFINITION */
const sessionModuleRoutes: Routes = [
  {
    path: 'sessions', component: SessionListComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'newsession', component: SessionAddComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
  {
    path: 'session/:id/edit', component: SessionEditComponent, canActivate: [AuthGuard],
    data: {
      role: [
        AccessLevel.manager
      ]
    }
  },
];

/** LOGIN ROUTES DEFINITION */
const loginRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(loginRoutes, { onSameUrlNavigation: 'reload' }),
    RouterModule.forRoot(movieModuleRoutes, { onSameUrlNavigation: 'reload' }),
    RouterModule.forRoot(cineRoomModuleRoutes, { onSameUrlNavigation: 'reload' }),
    RouterModule.forRoot(sessionModuleRoutes, { onSameUrlNavigation: 'reload' }),
    RouterModule.forRoot(userModuleRoutes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
})
export class AppRoutingModule { }
