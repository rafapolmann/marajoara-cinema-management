import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Session } from 'src/app/models/Session';
import { Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { SessionService } from 'src/app/services/SessionService';
import { firstValueFrom } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
import { DateTimeCustomFormat } from 'src/app/core/pipes/date-time-custom-format';

@Component({
  selector: 'app-session-list',
  templateUrl: './session-list.component.html',
  styleUrls: ['./session-list.component.scss'],
})
export class SessionListComponent implements OnInit {
  displayedColumns: string[] = [
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
  dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

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
    if (this.selectedSessionID === -1) return;

    if (!(await firstValueFrom(this.openDeleteDialog().afterClosed()))) return;

    if (
      await firstValueFrom(this.sessionService.delete(this.selectedSessionID))
    ) {
      const index = this.dataSource.data.indexOf(this.selectedSession, 0);
      if (index === -1) return;

      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  openDeleteDialog() {
    return this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Exclusão de sessão',
        message: `Deseja mesmo excluir a sessão? Filme: ${
          this.selectedSession.movie.title
        } - Data: ${this.dateTimeCustom.transform(
          this.selectedSession.sessionDate
        )}`,
        confirmText: 'Excluir',
      },
    });
  }
}
