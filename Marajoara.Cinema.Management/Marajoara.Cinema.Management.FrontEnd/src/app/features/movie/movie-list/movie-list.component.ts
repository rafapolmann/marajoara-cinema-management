import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { Movie } from 'src/app/Models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { ThisReceiver } from '@angular/compiler';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.scss'],
})
export class MovieListComponent implements OnInit,AfterViewInit {
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
  selectedMovie!:Movie;
  
  
  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild('dialogContent') dialogContent!: ConfirmDialogComponent;
  

  constructor(
    private movieService: MovieService,
    private router: Router,
    private dialog: MatDialog
  ) {}
  ngAfterViewInit(): void {
    
    
  }

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
    this.selectedMovie = row;
    
  }
  onAddClick() {
    this.router.navigateByUrl('newmovie');
  }

  onEditClick() {
    if (this.selectedMovieID === -1) return;
    this.router.navigateByUrl(`movie/${this.selectedMovieID}/edit`);
  }

 async onDeleteClick() {
    if (this.selectedMovieID === -1) return;
    const dlg =  this.dialog.open(ConfirmDialogComponent, {
//      width: '350px',       
    });

    if(!await firstValueFrom(dlg.afterClosed())) return;
    
    var success = await firstValueFrom(this.movieService.delete(this.selectedMovieID));
    if (success) {      
      const index = this.dataSource.data.indexOf(this.selectedMovie,0);      
      if (index > -1) {
        this.dataSource.data.splice(index, 1);
        this.dataSource._updateChangeSubscription();
      }
    }


  }
}
