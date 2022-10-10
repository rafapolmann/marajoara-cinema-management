import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

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
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { UserAccountListComponent } from './features/user/user-account-list/user-account-list.component';
import { UserAccountAddComponent } from './features/user/user-account-add/user-account-add.component';
import { UserAccountEditComponent } from './features/user/user-account-edit/user-account-edit.component';
import { UserAccountFormComponent } from './components/user/user-account-form/user-account-form.component';

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
        UserAccountListComponent,
        UserAccountAddComponent,
        UserAccountEditComponent,
        UserAccountFormComponent,
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
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: LoadingInterceptor,
        multi: true,
    },
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
    ],
    bootstrap: [AppComponent],
})
export class AppModule { }
