import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';

@Component({
  selector: 'app-cineroom-add',
  templateUrl: './cineroom-add.component.html',
  styleUrls: ['./cineroom-add.component.scss'],
})
export class CineroomAddComponent implements OnInit {
  constructor(
    private cineRoomService: CineRoomService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  navigateToList() {
    this.router.navigate(['/cinerooms']);
  }

  onSubmit(cineRoom: CineRoom) {
    this.saveMovie(cineRoom);
  }

  onCancel() {
    this.navigateToList();
  }

  async saveMovie(cineRoom: CineRoom) {
    try {
      cineRoom.cineRoomID = await firstValueFrom(
        this.cineRoomService.add(cineRoom)
      );
    } catch (exception: any) {
      this.showErrorMessage(exception);
    } finally {
      this.navigateToList();
    }
  }

  showErrorMessage(exception: any) {
    this.snackBar.open(
      `error status ${exception.status} - ${Object.values(exception.error)[0]}`,
      'Fechar',
      {
        verticalPosition: 'top',
        horizontalPosition: 'right',
      }
    );
  }
}
