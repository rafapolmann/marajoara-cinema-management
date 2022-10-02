import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators, } from '@angular/forms';
import { Session } from 'src/app/models/Session';
import { firstValueFrom, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';

@Component({
  selector: 'app-session-form',
  templateUrl: './session-form.component.html',
  styleUrls: ['./session-form.component.scss'],
})
export class SessionFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<Session>();
  @Output() onCancel = new EventEmitter();
  @Input() sessionData!: Session;

  sessionForm!: FormGroup;

  filteredMovies!: Observable<Movie[]>;
  movies: Movie[] = [];

  filteredCineRooms!: Observable<CineRoom[]>;
  cineRooms: CineRoom[] = [];

  constructor(private movieService: MovieService, private cineRoomService: CineRoomService) { }

  ngOnInit(): void {
    this.sessionForm = new FormGroup({
      sessionID: new FormControl(this.sessionData ? this.sessionData.sessionID : ''),
      sessionDate: new FormControl(this.sessionData ? this.sessionData.sessionDate : '', [Validators.required]),
      endSession: new FormControl(this.sessionData ? this.sessionData.endSession : '', [Validators.required]),
      price: new FormControl(this.sessionData ? this.sessionData.price : true, [Validators.required]),
      movieCtrl: new FormControl(this.sessionData ? this.sessionData.movie.title : '', [Validators.required]),
      cineRoomCtrl: new FormControl(this.sessionData ? this.sessionData.cineRoom.name : '', [Validators.required]),
    });

    this.loadMoviesInputFilter();
    this.loadCineRoomsInputFilter();
  }

  get movieCtrl(): FormControl {
    return this.sessionForm.get('movieCtrl')! as FormControl;
  }

  get cineRoomCtrl(): FormControl {
    return this.sessionForm.get('cineRoomCtrl')! as FormControl;
  }

  async loadMoviesInputFilter() {
    this.movies = await firstValueFrom(this.movieService.getAll());
    this.filteredMovies = this.movieCtrl.valueChanges.pipe(
      startWith(''),
      map((movie) => (movie ? this.filterMovies(movie) : this.movies.slice()))
    );
  }

  private filterMovies(value: string): Movie[] {
    return this.movies.filter((movie) =>
      movie.title.toLowerCase().includes(value.toLowerCase())
    );
  }

  async loadCineRoomsInputFilter() {
    this.cineRooms = await firstValueFrom(this.cineRoomService.getAll());
    this.filteredCineRooms = this.cineRoomCtrl.valueChanges.pipe(
      startWith(''),
      map((cineRoom) => (cineRoom ? this.filterCineRooms(cineRoom) : this.cineRooms.slice()))
    );
  }

  private filterCineRooms(value: string): CineRoom[] {
    return this.cineRooms.filter((cineRoom) =>
      cineRoom.name.toLowerCase().includes(value.toLowerCase())
    );
  }

  cancel() {
    this.onCancel.emit();
  }

  submit(formDirective: FormGroupDirective): void {
    console.log(this.sessionForm);
    if (this.sessionForm.invalid) {
      return;
    }
    this.onSubmit.emit(this.sessionForm.value);
    //Resets the form.
    this.sessionForm.reset();
    formDirective.resetForm();
  }
}
