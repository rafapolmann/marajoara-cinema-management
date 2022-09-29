import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CineRoom } from 'src/app/Models/CineRoom';
import { firstValueFrom } from 'rxjs';
import { CineRoomService } from 'src/app/services/CineRoomService';

@Component({
  selector: 'app-cineroom-edit',
  templateUrl: './cineroom-edit.component.html',
  styleUrls: ['./cineroom-edit.component.scss'],
})
export class CineroomEditComponent implements OnInit {
  cineRoomData!: CineRoom;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private cineRoomService: CineRoomService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadCineroom(id);
  }

  async loadCineroom(cineroomID: number) {
    this.cineRoomData = await firstValueFrom(
      this.cineRoomService.getById(cineroomID)
    );
  }

  async onSubmit(cineroom: CineRoom) {
    try {
      await firstValueFrom(this.cineRoomService.update(cineroom));
    } catch (exception: any) {
      console.log(exception);
      alert(
        `error status ${exception.status}; Message: ${
          Object.values(exception.error)[0]
        }`
      );
    } finally {
      this.navigateToList();
    }
  }
  onCancel() {
    this.navigateToList();
  }

  navigateToList(): void {
    this.router.navigateByUrl('/cineroom');
  }
}
