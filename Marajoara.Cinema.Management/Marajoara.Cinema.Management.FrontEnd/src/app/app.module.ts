import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { MaterialModule } from './core/material/material.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MovieFormComponent } from './components/movie/movie-form/movie-form.component';
import { MovieEditComponent } from './features/movie/movie-edit/movie-edit.component';
import { MovieAddComponent } from './features/movie/movie-add/movie-add.component';
import { MovieListComponent } from './features/movie/movie-list/movie-list.component';
import { MovieCardComponent } from './components/movie/movie-card/movie-card.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ConfirmDialogComponent } from './components/common/confirm-dialog/confirm-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    MovieFormComponent,
    MovieEditComponent,
    MovieAddComponent,
    MovieListComponent,
    MovieCardComponent,
    ConfirmDialogComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
