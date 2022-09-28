import { Component, ViewChild, OnInit } from '@angular/core';
import { Movie } from 'src/app/Models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss'],
})
export class MovieListComponent implements OnInit {
  displayedColumns: string[] = [
    'movieID',
    'title',
    'description',
    'isOriginalAudio',
    'is3D',
    'minutes',
  ];
  dataToDisplay: Movie[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);
  selectedMovieID: number = -1;

  @ViewChild('paginator') paginator!: MatPaginator;

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  async loadMovies() {
    this.dataToDisplay = await firstValueFrom(this.movieService.getAll());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: any): void {
    this.selectedMovieID = -1;
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }

  highlight(row: any) {
    this.selectedMovieID = row.movieID;
  }
}
