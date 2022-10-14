import {
  Component,
  OnInit,
  ViewChildren,
  ElementRef,
  AfterViewInit,
  QueryList,
  ViewChild,
} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { firstValueFrom } from 'rxjs';
import { DateTimeCustomFormat } from 'src/app/core/pipes/date-time-custom-format';
import {  MovieFull } from 'src/app/models/Movie';
import { MovieService } from 'src/app/services/MovieService';

@Component({
  selector: 'app-movie-in-theater',
  templateUrl: './movie-in-theater.component.html',
  styleUrls: ['./movie-in-theater.component.scss'],
})
export class MovieInTheaterComponent implements OnInit, AfterViewInit {
  dataToDisplay: MovieFull[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);
  
  minDate!: Date;
  maxDate!: Date;
  startDateFilter!:Date;
  endDateFilter!:Date;
  dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

   
  constructor(private movieService: MovieService) {}
  ngAfterViewInit(): void {}
  currentItemIndex = 0;
  ngOnInit(): void {
    this.loadMovies();
    this.minDate = new Date();
    this.maxDate = new Date();
    this.maxDate.setDate(this.maxDate.getDate() + 30);
  }

  async loadMovies() {
    this.dataToDisplay = await firstValueFrom(this.movieService.getInTheater());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    // this.dataSource.paginator = this.paginator;
    this.dataSource.filterPredicate = (movie: MovieFull, filter: string) =>
    movie.title.toLowerCase().indexOf(filter.trim().toLowerCase()) != -1 && this.compareSessionDates(movie);
  }
  applyFilter(event: any): void {
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }
  applyDateFilter(dateRangeStart: HTMLInputElement, dateRangeEnd: HTMLInputElement) {    
    this.startDateFilter = this.getFullDate(dateRangeStart.value, false);
    this.endDateFilter = this.getFullDate(dateRangeEnd.value,true);
    
    const oldValue = this.dataSource.filter;
    this.dataSource.filter = ' ' + Math.random();
    this.dataSource.filter = oldValue;

  }

  getFullDate(dateTxt:string,forceEndOfTheDay:boolean): Date {
    const _dt:string[] = dateTxt.split('/');
    const year:number = Number(_dt[2]);
    const month:number = Number(_dt[1]);
    const day:number = Number(_dt[0]);
    
    const fullDate = new Date(
      year,
      month,
      day,
      forceEndOfTheDay?23: 0,
      forceEndOfTheDay?59: 0,      
      forceEndOfTheDay?59: 0,      
      0
    );

    return fullDate;
  }

  compareSessionDates(movie:MovieFull):boolean{
    var found = true;
    console.log(this.startDateFilter);
    if(this.startDateFilter && ! movie.sessions.find(s=>  s.sessionDate >= this.startDateFilter))    {
       found = false;
    }
    if(this.endDateFilter && ! movie.sessions.find(s=>  s.sessionDate <= this.startDateFilter)){
       found = false;
    }
    return found;
  }

}
