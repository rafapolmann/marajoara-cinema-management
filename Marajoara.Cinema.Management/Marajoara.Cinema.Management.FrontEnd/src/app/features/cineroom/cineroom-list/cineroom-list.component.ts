import { Component, ViewChild, OnInit } from '@angular/core';
import { CineRoom } from 'src/app/Models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
import { ToastrService } from 'src/app/services/toastr.service';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-cineroom-list',
  templateUrl: './cineroom-list.component.html',
  styleUrls: ['./cineroom-list.component.scss'],
})
export class CineroomListComponent implements OnInit {
  displayedColumns: string[] = ['cineRoomID', 'name', 'totalSeats'];
  dataToDisplay: CineRoom[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);
  selectedCineRoomID: number = -1;
  selectedCineRoom!: CineRoom;

  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(
    private cineRoomService: CineRoomService,
    private router: Router,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadCineRooms();
  }

  async loadCineRooms() {
    this.dataToDisplay = await firstValueFrom(this.cineRoomService.getAll());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: any): void {
    this.selectedCineRoomID = -1;
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }

  highlight(row: any) {
    this.selectedCineRoomID = row.cineRoomID;
    this.selectedCineRoom = row;
  }
  onAddClick() {
    this.router.navigateByUrl('newcineroom');
  }

  onEditClick() {
    if (this.selectedCineRoomID === -1) return;
    this.router.navigateByUrl(`cineroom/${this.selectedCineRoomID}/edit`);
  }

  async onDeleteClick() {
    if (this.selectedCineRoomID === -1) return;

    if (!(await firstValueFrom(this.openDeleteDialog().afterClosed()))) return;

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
    if (
      await firstValueFrom(this.cineRoomService.delete(this.selectedCineRoomID))
    ) {
      const index = this.dataSource.data.indexOf(this.selectedCineRoom, 0);
      if (index === -1) return;

      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  openDeleteDialog() {
    return this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Exclusão de sala',
        message: `Deseja mesmo excluir a sala ${this.selectedCineRoom.name}?`,
        confirmText: 'Excluir',
      },
    });
  }
}
