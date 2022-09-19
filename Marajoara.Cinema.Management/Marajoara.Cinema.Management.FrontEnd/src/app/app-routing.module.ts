import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';


const routes: Routes = [
  { path: '', component: MovieListComponent},    
  { path: 'movie/:id/edit', component: MovieEditComponent },
  { path: 'newmovie', component: MovieAddComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation:'reload'})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
