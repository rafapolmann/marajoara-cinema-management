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
import { CineroomListComponent } from './features/cineroom/cineroom-list/cineroom-list.component';
import { CineroomAddComponent } from './features/cineroom/cineroom-add/cineroom-add.component';
import { CineroomSeatPickerComponent } from './components/cineroom/cineroom-seat-picker/cineroom-seat-picker.component';
import { CineroomFormComponent } from './components/cineroom/cineroom-form/cineroom-form.component';
import { CineroomEditComponent } from './features/cineroom/cineroom-edit/cineroom-edit.component';
import { SessionListComponent } from './features/session/session-list/session-list.component';
import { DateTimeCustomFormat } from './core/pipes/date-time-custom-format';
import { SessionAddComponent } from './features/session/session-add/session-add.component';
import { SessionFormComponent } from './components/session/session-form/session-form.component';
import { SessionEditComponent } from './features/session/session-edit/session-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    MovieFormComponent,
    MovieEditComponent,
    MovieAddComponent,
    MovieListComponent,
    MovieCardComponent,
    ConfirmDialogComponent,
    CineroomListComponent,
    CineroomAddComponent,
    CineroomSeatPickerComponent,
    CineroomFormComponent,
    CineroomEditComponent,
    SessionListComponent,
    DateTimeCustomFormat,
    SessionAddComponent,
    SessionFormComponent,
    SessionEditComponent,
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
export class AppModule { }
