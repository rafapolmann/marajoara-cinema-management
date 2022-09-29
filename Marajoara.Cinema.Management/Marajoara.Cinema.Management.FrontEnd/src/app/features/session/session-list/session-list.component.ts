import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Session } from 'src/app/models/Session';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SessionService } from 'src/app/services/SessionService';
import { firstValueFrom } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-session-list',
  templateUrl: './session-list.component.html',
  styleUrls: ['./session-list.component.scss'],
})
export class SessionListComponent implements OnInit {
  displayedColumns: string[] = [
    //'sessionID',
    'movieTitle',
    'cineRoomName',
    'sessionDate',
    'endSessionDate',
    'price',
  ];
  dataToDisplay: Session[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);

  selectedSessionID: number = -1;
  selectedSession!: Session;

  @ViewChild('paginator') paginator!: MatPaginator;

  constructor(
    private sessionService: SessionService,
    private router: Router,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadSessions();
  }

  async loadSessions() {
    this.dataToDisplay = await firstValueFrom(this.sessionService.getAll());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);

    this.dataSource.paginator = this.paginator;
  }

  formatDate(date: Date): string {
    const parsedDate = new Date(date);
    return parsedDate.toLocaleString([], {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  formatPrice(price: Number): string {
    return `R$ ${price.toFixed(2)}`;
  }

  highlight(row: any) {
    this.selectedSessionID = row.sessionID;
    this.selectedSession = row;
  }

  applyFilter(event: any): void {
    // this.selectedMovieID = -1;
    // this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }

  onAddClick() {
    //this.router.navigateByUrl('newmovie');
  }

  onEditClick() {
    // if (this.selectedMovieID === -1) return;
    // this.router.navigateByUrl(`movie/${this.selectedMovieID}/edit`);
  }

  async onDeleteClick() {
    // if (this.selectedMovieID === -1) return;
    // if (!(await firstValueFrom(this.openDeleteMovieDialog().afterClosed())))
    //   return;
    // if (await firstValueFrom(this.movieService.delete(this.selectedMovieID))) {
    //   const index = this.dataSource.data.indexOf(this.selectedMovie, 0);
    //   if (index === -1) return;
    //   this.dataSource.data.splice(index, 1);
    //   this.dataSource._updateChangeSubscription();
    // }
  }
}
