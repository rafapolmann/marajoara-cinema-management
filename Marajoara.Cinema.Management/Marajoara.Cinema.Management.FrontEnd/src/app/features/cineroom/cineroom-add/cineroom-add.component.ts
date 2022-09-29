import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { CineRoom } from 'src/app/models/CineRoom';
import { CineRoomService } from 'src/app/services/CineRoomService';

@Component({
  selector: 'app-cineroom-add',
  templateUrl: './cineroom-add.component.html',
  styleUrls: ['./cineroom-add.component.scss']
})
export class CineroomAddComponent implements OnInit {
  constructor(private cineRoomService: CineRoomService, private router: Router) {}

  ngOnInit(): void {}
  
  navigateToList(){
    this.router.navigate(['/cineroom']);
  }
  
  onSubmit(cineRoom: CineRoom) {
    this.saveMovie(cineRoom);
  }

  onCancel(){
    this.navigateToList();
  }
 
  async saveMovie(cineRoom: CineRoom) {
    try {
      cineRoom.cineRoomID = await firstValueFrom(this.cineRoomService.add(cineRoom));      
    } catch (exception: any) {
      console.log(exception);
      alert(
        `error status ${exception.status}; Message: ${
          Object.values(exception.error)[0]
        }`
      );
    }finally{
      this.navigateToList();
    }
  }
}