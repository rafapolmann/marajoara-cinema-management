import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormGroupDirective,
  Validators,
} from '@angular/forms';
import { Session, SessionCommand } from 'src/app/models/Session';
import { firstValueFrom, Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Movie } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';
import { DateTimeCustomFormat } from 'src/app/core/pipes/date-time-custom-format';

@Component({
  selector: 'app-session-form',
  templateUrl: './session-form.component.html',
  styleUrls: ['./session-form.component.scss'],
})
export class SessionFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<SessionCommand>();
  @Output() onCancel = new EventEmitter();
  @Input() sessionData!: Session;

  sessionForm!: FormGroup;

  dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

  filteredMovies!: Observable<Movie[]>;
  movies: Movie[] = [];

  filteredCineRooms!: Observable<CineRoom[]>;
  cineRooms: CineRoom[] = [];

  constructor(
    private movieService: MovieService,
    private cineRoomService: CineRoomService
  ) { }

  ngOnInit(): void {
    this.sessionForm = new FormGroup({
      sessionID: new FormControl(this.sessionData ? this.sessionData.sessionID : ''),
      sessionDate: new FormControl(this.sessionData ? this.sessionData.sessionDate : ''.toLocaleString(), [Validators.required]),
      sessionTime: new FormControl(this.sessionData ? this.dateTimeCustom.transformToTime(this.sessionData.sessionDate) : '', [Validators.required]),
      endSession: new FormControl(this.sessionData ? this.dateTimeCustom.transform(this.sessionData.endSession!) : ''),
      price: new FormControl(this.sessionData ? this.sessionData.price.toFixed(2) : '0.00', [Validators.required]),
      movieCtrl: new FormControl(this.sessionData ? this.sessionData.movie : '', [Validators.required, this.RequireMatch]),
      cineRoomCtrl: new FormControl(this.sessionData ? this.sessionData.cineRoom : '', [Validators.required, this.RequireMatch]),
    });

    this.loadMoviesInputFilter();
    this.loadCineRoomsInputFilter();
  }

  get sessionID(): FormControl {
    return this.sessionForm.get('sessionID')! as FormControl;
  }

  get sessionDate(): FormControl {
    return this.sessionForm.get('sessionDate')! as FormControl;
  }

  get sessionTime(): FormControl {
    return this.sessionForm.get('sessionTime')! as FormControl;
  }

  get endSession(): FormControl {
    return this.sessionForm.get('endSession')! as FormControl;
  }

  get price(): FormControl {
    return this.sessionForm.get('price')! as FormControl;
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
      map(() => this.filterMovies())
    );
  }

  private filterMovies(): Movie[] {
    if (!this.movieCtrl.value)
      return this.movies.slice();

    var value = this.movieCtrl.value;
    value = typeof value === 'string' ? value : value!.title;

    if (!value)
      return this.movies.slice();

    return this.movies.filter((movie) =>
      movie.title.toLowerCase().includes(value.toLowerCase())
    );
  }

  async loadCineRoomsInputFilter() {
    this.cineRooms = await firstValueFrom(this.cineRoomService.getAll());
    this.filteredCineRooms = this.cineRoomCtrl.valueChanges.pipe(
      startWith(''),
      map(() => this.filterCineRooms())
    );
  }

  private filterCineRooms(): CineRoom[] {
    if (!this.cineRoomCtrl.value)
      return this.cineRooms.slice();

    var value = this.cineRoomCtrl.value;
    value = typeof value === 'string' ? value : value!.name;

    if (!value)
      return this.cineRooms.slice();

    return this.cineRooms.filter((cineRoom) =>
      cineRoom.name.toLowerCase().includes(value.toLowerCase())
    );
  }

  cancel() {
    this.onCancel.emit();
  }

  submit(formDirective: FormGroupDirective): void {
    if (this.sessionForm.invalid)
      return;

    const sessionToEmit = this.getSessionToSubmit();
    this.onSubmit.emit(sessionToEmit);

    //Resets the form.
    this.sessionForm.reset();
    formDirective.resetForm();
  }


  getSessionToSubmit(): SessionCommand {
    const sessionCommand: SessionCommand = {
      sessionID: this.sessionID.value,
      price: this.price.value,
      sessionDate: this.getFullDate().toISOString(),
      movieID: this.movieCtrl.value.movieID,
      cineRoomID: this.cineRoomCtrl.value.cineRoomID
    };

    return sessionCommand;
  }

  priceChange() {
    if (this.price.valid) {
      this.price.setValue(this.price.value.toFixed(2));
    }
  }

  getFullDate(): Date {
    const _date: Date = new Date(this.sessionDate.value);
    const time: number[] = this.sessionTime.value.split(':');
    const fullDate = new Date(
      _date.getFullYear(),
      _date.getMonth(),
      _date.getDate(),
      time[0],
      time[1],
      0,
      0
    );

    return fullDate;
  }


  endSessionChange() {
    if (this.movieCtrl.valid && this.sessionDate.valid && this.sessionTime.valid) {
      const movieDuration: number = this.movieCtrl.value.minutes;
      const _date: Date = new Date(this.sessionDate.value);
      const time: number[] = this.sessionTime.value.split(':');
      const fullDate = this.getFullDate();

      fullDate.setMinutes(fullDate.getMinutes() + movieDuration);
      this.endSession.setValue(this.dateTimeCustom.transform(fullDate));
    } else
      this.endSession.setValue("");
  }

  movieAutoCompleteDisplayWith(obj?: Movie): string {
    return obj ? obj.title : '';
  }

  cineRoomAutoCompleteDisplayWith(obj?: CineRoom): string {
    return obj ? obj.name : '';
  }

  RequireMatch(control: AbstractControl) {
    const selection: any = control.value;
    if (typeof selection === 'string') {
      return { incorrect: true };
    }
    return null;
  }
}
