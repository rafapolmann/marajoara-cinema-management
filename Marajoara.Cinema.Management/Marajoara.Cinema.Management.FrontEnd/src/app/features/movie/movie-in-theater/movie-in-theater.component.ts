import { formatDate } from '@angular/common';
import {
  Component,
  OnInit,
  AfterViewInit,
} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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
  dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

   
  filterForm = new FormGroup({
    startDate: new FormControl(),
    endDate: new FormControl(),
    text: new FormControl(),
  });


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
    this.dataToDisplay.map(m=> m.sessions.map(s=> {
      s.sessionDate = new Date(s.sessionDate);
    }));

    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.dataSource.filterPredicate = (movie: MovieFull, filter: string) =>
    movie.title.toLowerCase().indexOf(filter.trim().toLowerCase()) != -1 && this.compareSessionDates(movie);
  }

  applyFilter(){    
    this.dataSource.filter = this.textFilter;
  }
  
  applyDateFilter(){
    if(!this.dataSource.filter){
      this.dataSource.filter = ' ';
    }else{
      this.dataSource.filter += ' ';
    }

  }
  
  clearFilters(){
    this.filterForm.reset();
    this.applyFilter();
  }

  compareSessionDates(movie:MovieFull):boolean{
    var found = true;    

    if(this.startDateFilter && this.startDateFilter.valueOf() && ! movie.sessions.find(s=>  s.sessionDate >= this.startDateFilter!))    {
       found = false;
    }
    if(this.endDateFilter && this.endDateFilter.valueOf() && ! movie.sessions.find(s=>  s.sessionDate <= this.endDateFilter!)){
       found = false;
   }       
    return found;
  }

  get textFilter():string{
    return this.filterForm.get('text')!.value;
  }
  get startDateFilter(): Date| undefined {
    if(!this.filterForm.get('startDate')!.value){
      return undefined;
    }
    return this.filterForm.get('startDate')!.value;
  }

  get endDateFilter(): Date | undefined {
    if(!this.filterForm.get('endDate')!.value){
      return undefined;
    }
    const baseDate = new Date(  this.filterForm.get('endDate')!.value);
    
    
    const date = new Date(
      baseDate.getFullYear(),
      baseDate.getMonth(),
      baseDate.getDate(),
      23,59,59,0
    )

    return date;
  }


}
