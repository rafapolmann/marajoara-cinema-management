import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';
import { TotastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-cineroom-add',
  templateUrl: './cineroom-add.component.html',
  styleUrls: ['./cineroom-add.component.scss'],
})
export class CineroomAddComponent implements OnInit {
  constructor(
    private cineRoomService: CineRoomService,
    private router: Router,
    private toastr: TotastrService
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
      this.toastr.showErrorMessage(`error status ${exception.status} - ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }

}
