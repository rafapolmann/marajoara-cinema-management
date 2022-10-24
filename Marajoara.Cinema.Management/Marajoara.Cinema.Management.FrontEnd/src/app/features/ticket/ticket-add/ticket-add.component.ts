import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, firstValueFrom, map, Observable, startWith } from 'rxjs';
import { MovieFull } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { SessionFlat, SessionSeat } from 'src/app/models/Session';
import { DateTimeCustomFormat } from 'src/app/core/pipes/date-time-custom-format';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';
import { SessionService } from 'src/app/services/SessionService';
import { ToastrService } from 'src/app/services/toastr.service';
import { TicketService } from 'src/app/services/ticket.service';
import { ThisReceiver } from '@angular/compiler';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-ticket-add',
  templateUrl: './ticket-add.component.html',
  styleUrls: ['./ticket-add.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { showError: true }
    }
  ]
})
export class TicketAddComponent implements OnInit {
  @ViewChild(MatStepper) stepper!: MatStepper;
  movies!: MovieFull[];
  filteredMovies!: Observable<MovieFull[]>;
  filteredSessionsSubject: BehaviorSubject<SessionFlat[]> = new BehaviorSubject<SessionFlat[]>([]);
  filteredSessions: Observable<SessionFlat[]> = this.filteredSessionsSubject.asObservable();
  cineRoom: CineRoom | undefined;
  occupiedSeats: number[] = [];
  
  private preSelectedSessionID: number | undefined;
  private dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

  movieForm!: FormGroup;
  sessionForm!: FormGroup;
  seatForm!: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private movieService: MovieService,
    private sessionService: SessionService,
    private cineRoomService: CineRoomService,
    private ticketService: TicketService,
    private toastrService: ToastrService,
  ) {
    this.movieForm = new FormGroup({
      movie: new FormControl(undefined, [Validators.required, this.RequireMatch]),
    });
    this.sessionForm = new FormGroup({
      session: new FormControl(undefined, [Validators.required, this.RequireMatch]),
    });
    this.seatForm = new FormGroup({
      seat: new FormControl(-1, [Validators.required, Validators.min(1)]),
    });
  }


  ngOnInit(): void {
    this.loadData();
  }
  async loadData() {
    this.loadMovies();
    this.setSessionBehavior();
    this.setCineRoomBehavior();

    if (!!this.route.snapshot.queryParams['sessionId']) {
      this.preSelectedSessionID = Number(this.route.snapshot.queryParams['sessionId']);
      const preSession = await firstValueFrom(this.sessionService.getById(this.preSelectedSessionID));
      const _movie = this.movies.find(m => m.movieID === preSession.movie.movieID);
      if (!_movie)
        return;
      
      this.movie.setValue(_movie);
      this.stepper.next();
      const _session = _movie!.sessions.find(s => s.sessionID === preSession.sessionID);
      if (!_session)
        return;
      
        this.session.setValue(_session);
        this.stepper.next();
    }
  }

  setSessionBehavior() {
    this.movie.valueChanges.subscribe(m => {
      this.filteredSessionsSubject.next(m.sessions)
      this.sessionForm.reset();
      this.seatForm.reset();
    });
    this.session.valueChanges.subscribe(() => this.filteredSessionsSubject.next(this.filterSessions()));

  }
  setCineRoomBehavior() {
    this.session.valueChanges.subscribe(() => this.loadCineRoom());
  }


  get movie(): FormControl {
    return this.movieForm.get('movie')! as FormControl;
  }
  get session(): FormControl {
    return this.sessionForm.get('session')! as FormControl;
  }

  get seat(): FormControl {
    return this.seatForm.get('seat')! as FormControl;
  }

  get selectedMovie(): MovieFull | undefined {
    return this.movie.value as MovieFull;
  }

  get selectedSession(): SessionFlat | undefined {
    return this.session.value;
  }

  get movieStepText(): string {
    if (this.movieForm.valid)
      return `Filme: ${this.movie.value.title}`;

    return 'Filme'
  }

  get sessionStepText(): string {
    if (this.sessionForm.valid)
      return `Sessão: ${this.dateTimeCustom.transform(this.selectedSession!.sessionDate)}`;

    return 'Sessão'
  }

  get seatStepText(): string {
    if (this.sessionForm.valid && this.seat!.value > 0)
      return `Assento: ${this.seat!.value}`;

    return 'Assento'
  }

  async loadCineRoom() {
    if (!this.selectedSession || typeof this.selectedSession === 'string')
      this.cineRoom = undefined;
    else {
      this.loadOccupiedSeats();
      this.cineRoom = await firstValueFrom(this.cineRoomService.getById(this.selectedSession.cineRoomID));
    }
  }

  async loadOccupiedSeats() {
    if (this.selectedSession) {
      const seatsRet: SessionSeat[] = await firstValueFrom(this.sessionService.getOccupiedSeats(this.selectedSession.sessionID));
      this.occupiedSeats = seatsRet.map(s => s.seatNumber);
    } else {
      this.occupiedSeats = [];
    }
  }

  async loadMovies() {
    this.movies = await firstValueFrom(this.movieService.getInTheater());
    this.filteredMovies = this.movie.valueChanges.pipe(
      startWith(''),
      map(() => this.filterMovies())
    );
  }
  private filterMovies(): MovieFull[] {

    if (!this.movie.value)
      return this.movies.slice();

    var value = this.movie.value;
    value = typeof value === 'string' ? value : value!.title;

    if (!value)
      return this.movies.slice();

    return this.movies.filter((movie) =>
      movie.title.toLowerCase().includes(value.toLowerCase())
    );

  }

  private filterSessions(): SessionFlat[] {
    if (!this.selectedMovie ||  typeof this.selectedMovie === 'string')
      return [];

    if (!this.session.value)
      return this.selectedMovie!.sessions.slice();

    var value = this.session.value;
    value = typeof value === 'string' ? value : this.dateTimeCustom.transform(value!.sessionDate);

    if (!value)
      return this.selectedMovie!.sessions.slice();

    return this.selectedMovie!.sessions.filter((session) =>
      this.dateTimeCustom.transform(session.sessionDate).includes(value)
    );
  }

  onSelectedSeat(seat: number) {
    this.seat.setValue(seat);
  }

  async onSubmit() {    
    if (this.movieForm.invalid || this.sessionForm.invalid || this.seatForm.invalid)
      return;

    await firstValueFrom(this.ticketService.add(this.selectedSession!.sessionID, this.seat!.value));
    this.toastrService.showErrorMessage('Ticket finalizado com sucesso!', 'fechar');
    this.router.navigateByUrl('mytickets');
  }

  movieAutoCompleteDisplayWith(obj?: MovieFull): string {
    return obj ? obj.title : '';
  }

  sessionAutoCompleteDisplayWith(s?: SessionFlat): string {    
    if (!s || !s.sessionDate)
      return '';
    return new DateTimeCustomFormat().transform(s.sessionDate);
  }

  RequireMatch(control: AbstractControl) {
    const selection: any = control.value;
    if (typeof selection === 'string') {
      return { incorrect: true };
    }
    return null;
  }
}