import { Component, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { Movie } from 'src/app/Models/Movie';
import { MovieService } from 'src/app/services/MovieService';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
import { ToastrService } from 'src/app/services/toastr.service';
import { MatSort } from '@angular/material/sort';
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
  selectedMovie!: Movie;

  @ViewChild('paginator') paginator!: MatPaginator;  
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private movieService: MovieService,
    private router: Router,
    private dialog: MatDialog,
    private toastr: ToastrService,
  ) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  async loadMovies() {
    this.dataToDisplay = await firstValueFrom(this.movieService.getAll());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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

    if (!(await firstValueFrom(this.openDeleteMovieDialog().afterClosed())))
      return;

    try {
      await this.deleteSelected();
    } catch (exception: any) {
      this.toastr.showErrorMessage(
        `error status ${exception.status} - ${
          Object.values(exception.error)[0]
        }`
      );
    }
  }
  async deleteSelected() {
    if (await firstValueFrom(this.movieService.delete(this.selectedMovieID))) {
      const index = this.dataSource.data.indexOf(this.selectedMovie, 0);
      if (index === -1) return;

      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }
  openDeleteMovieDialog() {
    return this.dialog.open(ConfirmDialogComponent, {      
      data: {
        title: 'Exclusão de filme',
        message: `Deseja mesmo excluir o filme ${this.selectedMovie.title}?`,
        confirmText: 'Excluir',
      },
    });
  }
}
